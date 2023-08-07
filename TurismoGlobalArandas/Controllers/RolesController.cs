using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using UConnect.Entities;
using UConnect.Entities.Identity;

namespace UConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public RolesController(
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            TurismoGlobalContext context
        )
        {
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }
        [HttpGet("{Name}")]
        public async Task<IActionResult> GetRoles(string Name)
        {
            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(f => f.Name == Name);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        [HttpPost("{Name}")]
        public async Task<IActionResult> postRole(string Name)
        {
            if (!await _roleManager.RoleExistsAsync(Name))
                await _roleManager.CreateAsync(new IdentityRole(Name));
            return Ok("Role creado correctamente"); 
        }
        [HttpPut("{Name}/{newRoleName}")]
        public async Task<IActionResult> putRole(string Name, string newRoleName)
        {
            if (!await _roleManager.RoleExistsAsync(Name))
            {
                return NotFound();
            }
            var role = await _roleManager.FindByNameAsync(Name);
               role.Name= newRoleName;
            await _roleManager.UpdateAsync(role);

            return Ok("Role modificado correctamente");

        }
        [HttpDelete("{RoleId}")]
        public async Task<IActionResult> DeleteUser(string RoleId)
        {
            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(w => w.Id == RoleId);
            if (role == null)
            {
                return BadRequest();
            }
            await _roleManager.DeleteAsync(role);

            return Ok("Role eliminado correctamente");
        }
    }
}
