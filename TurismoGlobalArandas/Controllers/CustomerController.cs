using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public CustomerController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Customers>> getCustomers()
        {
            var customers = await _context.Customers
                .Where(w => !w.isDeleted)
                .ToListAsync();
            return Ok(customers);
        }
        [HttpGet("{CustomerId}")]
        public async Task<ActionResult> getCustomerById(int CustomerId)
        {
            var customer = await _context.Customers
                .Where(w => !w.isDeleted)
                .FirstOrDefaultAsync(f => f.CustomerId == CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<Customers>> PostCustomer(Customers customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getCustomerById", new { CustomerId = customer.CustomerId }, customer);
        }
        [HttpPut("{CustomerId}")]
        public async Task<ActionResult> PutCustomer(int CustomerId, Customers customers)
        {
            if (customers.CustomerId != CustomerId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var customerOld = await _context.Customers.FirstOrDefaultAsync(f => f.CustomerId == CustomerId);
            if (customerOld == null)
            {
                return BadRequest($"El cliente con el ID { CustomerId } no existe");
            }
            customerOld.CustomerId = customers.CustomerId;
            customerOld.Name = customers.Name;
            customerOld.Lastname = customers.Lastname;
            customerOld.PhoneNumber = customers.PhoneNumber;
            customerOld.isDeleted = customers.isDeleted;

            _context.Customers.Update(customerOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }
        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(f => f.CustomerId == CustomerId);
            if (customer == null)
            {
                return NotFound();
            }

            customer.isDeleted = true;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok("Cliente archivado");
        }
    }
}        
