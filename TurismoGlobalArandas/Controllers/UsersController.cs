using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UConnect.Entities.Identity;
using UConnect.Entities;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Migrations;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        private readonly UserManager<User> _userManager;
        private IPasswordHasher<User> passwordHasher;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UsersController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            TurismoGlobalContext context,
            IPasswordHasher<User> passwordHash
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            passwordHasher = passwordHash;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Include(i => i.Employee)
                .ToListAsync();
            return Ok(users);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var password =  _userManager.PasswordHasher.ToString();
            var userRole = await _userManager.GetRolesAsync(user);
            var roleId = await _roleManager.FindByNameAsync(userRole[0]);
            if (user == null)
            {
                return NotFound("No se encuentran usuario");
            }
            return Ok(new
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = userRole[0],
                RoleId = roleId.Id,
                EmployeeId = user.EmployeeId,
                Password = password,
                Status = 200
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = CreateToken(authClaims);
                    var refreshToken = GenerateRefreshToken();

                    _ = int.TryParse(
                        _configuration["JWT:RefreshTokenValidityInDays"],
                        out int refreshTokenValidityInDays
                    );

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                    await _userManager.UpdateAsync(user);
                    return Ok(
                        new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            RefreshToken = refreshToken,
                            Expiration = token.ValidTo,
                            Role = userRoles[0],
                            UserName = user.UserName,
                            Status = 200
                        }
                    );
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterOperador([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "El usuario ya existe" }
                );

            User user =
                new()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    Email = model.Email,
                    EmployeeId = model.EmployeeId,
                };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    }
                );
            }
            var Role = await _roleManager.FindByIdAsync(model.RoleId);
            if (Role == null)
            {
                return BadRequest("No se encuentra el rol ingresado");
            }
            await _userManager.AddToRoleAsync(user, Role.Name);
            return Ok(
                new Response { Status = "Success", Message = "Usuario registrado correctamente!" }
            );
        }

        [HttpPut("{UserName}")]
        public async Task<IActionResult> UpdateUser(string UserName, [FromBody] RegisterModel model)
        {
            if (model.Username != UserName)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return NotFound("No se encuentra el usuario ingresado");
            }

            user.UserName = model.Username;
            user.Email = model.Email;
            user.EmployeeId = model.EmployeeId;
            var role = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRoleAsync(user, role[0]);
            var newRole = await _roleManager.FindByIdAsync(model.RoleId);
            await _userManager.AddToRoleAsync(user, newRole.Name);

            await _userManager.UpdateAsync(user);
            return Ok("Usuario actualizado correctamente");
        }

        [HttpPut("{UserName}/Password/{CurrentPassword}/{NewPassword}")]
        public async Task<IActionResult> UpdatePasswordUser(
            string UserName,
            string CurrentPassword,
            string NewPassword
        )
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return NotFound();
            }
            if (CurrentPassword.Equals(NewPassword))
            {
                return BadRequest("Su contraseña nueva no puede ser su contraseña anterior");
            }
            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }
            string username = principal.Identity.Name;
            if(username == null || username.Length == 0)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByNameAsync(username);

            if (
                user == null
                || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now
            )
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(
                new
                {
                    accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    refreshToken = newRefreshToken
                }
            );
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(w => w.UserName == userName);
            if (user == null)
            {
                return BadRequest();
            }
            await _userManager.DeleteAsync(user);

            return Ok("Usuario eliminado correctamente");
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
            );
            _ = int.TryParse(
                _configuration["JWT:TokenValidityInMinutes"],
                out int tokenValidityInMinutes
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
                ),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken
            );
            if (
                securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
