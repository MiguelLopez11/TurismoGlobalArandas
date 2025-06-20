﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Entities;
using TurismoGlobalArandas.Models;
using TurismoGlobalArandas.Models.Identity;

namespace UConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        private readonly UserManager<User> _userManager;

        public EmployeesController(TurismoGlobalContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<Employees>> getEmpleados()
        {
            var employee = await _context.Employees.Where(w => !w.IsDeleted).ToListAsync();
            return Ok(employee);
        }
        [HttpPost("ReservationsByEmployee/{EmployeeId}")]
        public async Task<ActionResult> ObtenerDatosDesdeVista(
            int EmployeeId,
            [FromBody] List<DateTime> dates
        )
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(f => f.EmployeeId == EmployeeId);
                if (user == null)
                {
                    return BadRequest();
                }
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole[0].Equals("ADMINISTRADOR"))
                {
                    var datosVista = _context.ReservationsByEmployeeView
                        .FromSqlRaw("SELECT * FROM All_Reservations")
                        .AsEnumerable()
                        .Where(
                            r =>
                                dates == null
                                || dates.Count == 0
                                || (
                                    dates.Any(date => r.DateSale == date.Date)
                                    || (
                                        r.DateSale >= dates[0].Date
                                        && r.DateSale <= dates[dates.Count - 1].Date
                                    )
                                )
                        )
                        .ToList();

                    return Ok(datosVista);
                }
                var result = _context.ReservationsByEmployeeView
                    .FromSqlInterpolated($"EXEC ReservationsByEmployee {EmployeeId}")
                    .AsEnumerable()
                    .Where(
                        r =>
                            dates == null
                            || dates.Count == 0
                            || (r.DateSale >= dates[0] && r.DateSale <= dates[dates.Count - 1])
                    )
                    .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                var result = Array.Empty<ReservationsByEmployeeView>();
                return Ok(result);
            }
        }

        [HttpGet("{EmployeeId}")]
        public async Task<ActionResult> GetEmpleadosById(int EmployeeId)
        {
            var employee = await _context.Employees
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.EmployeeId == EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmpleados(Employees employees)
        {
            _context.Employees.Add(employees);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "GetEmpleadosById",
                new { EmployeeId = employees.EmployeeId },
                employees
            );
        }

        [HttpPut("{EmployeeId}")]
        public async Task<ActionResult> PutEmpleados(int EmployeeId, Employees employees)
        {
            if (employees.EmployeeId != EmployeeId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var employee = await _context.Employees.FirstOrDefaultAsync(
                f => f.EmployeeId == EmployeeId
            );
            if (employee == null)
            {
                return BadRequest("El empleado no existe");
            }
            employee.EmployeeId = employees.EmployeeId;
            employee.Name = employees.Name;
            employee.Lastname = employees.Lastname;
            employee.WorkStation = employees.WorkStation;
            employee.Address = employees.Address;
            employee.PhoneNumber = employees.PhoneNumber;
            employee.Salary = employees.Salary;
            employee.IsDeleted = employees.IsDeleted;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return Ok("El empleado se actualizo correctamente");
        }

        [HttpDelete("{EmployeeId}")]
        public async Task<IActionResult> DeleteEmpleados(int EmployeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(
                f => f.EmployeeId == EmployeeId
            );
            if (employee == null)
            {
                return NotFound();
            }

            employee.IsDeleted = true;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return Ok("Empleado archivado");
        }

    }
}
