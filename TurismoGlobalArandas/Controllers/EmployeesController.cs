using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace UConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public EmployeesController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Employees>> getEmpleados()
        {
            var employee = await _context.Employees
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(employee);
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
            return CreatedAtAction("GetEmpleadosById", new { EmployeeId = employees.EmployeeId }, employees);
        }
        [HttpPut("{EmployeeId}")]
        public async Task<ActionResult> PutEmpleados(int EmployeeId, Employees employees)
        {
            if (employees.EmployeeId != EmployeeId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var employee = await _context.Employees.FirstOrDefaultAsync(f => f.EmployeeId == EmployeeId);
            if (employee == null)
            {
                return BadRequest("El empleado no existe");
            }
            employee.EmployeeId = employees.EmployeeId;
            employee.Name = employees.Name;
            employee.Lastname = employees.Lastname;
            employee.WorkStation = employees.WorkStation;
            employee.Email = employees.Email;
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
            var employee = await _context.Employees
                .FirstOrDefaultAsync(f => f.EmployeeId == EmployeeId);
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
