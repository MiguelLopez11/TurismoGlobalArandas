using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseFixedController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public ExpenseFixedController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ExpensesFixed>> getExpensesFixed()
        {
            var expenses = await _context.ExpensesFixeds 
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(expenses);
        }
        [HttpGet("{ExpensesFixedId}")]
        public async Task<ActionResult> getExpenseFixedById(int ExpensesFixedId)
        {
            var expense = await _context.ExpensesFixeds
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ExpensesFixedId == ExpensesFixedId);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }
        [HttpPost]
        public async Task<ActionResult<ExpensesFixed>> PostExpenseFixed(ExpensesFixed expense)
        {
            _context.ExpensesFixeds.Add(expense);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getExpenseFixedById", new { ExpensesFixedId = expense.ExpensesFixedId }, expense);
        }
        [HttpPut("{ExpensesFixedId}")]
        public async Task<ActionResult> PutExpenseFixed(int ExpensesFixedId, ExpensesFixed expense)
        {
            if (expense.ExpensesFixedId != ExpensesFixedId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ExpenseOld = await _context.ExpensesFixeds
                .FirstOrDefaultAsync(f => f.ExpensesFixedId == ExpensesFixedId);
            if (ExpenseOld == null)
            {
                return BadRequest($"El cliente con el ID {ExpensesFixedId} no existe");
            }
            ExpenseOld.ExpensesFixedId = expense.ExpensesFixedId;
            ExpenseOld.Name = expense.Name;
            ExpenseOld.Cost = expense.Cost;
            ExpenseOld.Description = expense.Description;
            ExpenseOld.IsDeleted = expense.IsDeleted;

            _context.ExpensesFixeds.Update(ExpenseOld);
            await _context.SaveChangesAsync();
            return Ok("El gasto se actualizo correctamente");
        }
        [HttpDelete("{ExpensesFixedId}")]
        public async Task<IActionResult> DeleteExpenseFixed(int ExpensesFixedId)
        {
            var expense = await _context.ExpensesFixeds
                .FirstOrDefaultAsync(f => f.ExpensesFixedId == ExpensesFixedId);
            if (expense == null)
            {
                return NotFound();
            }

            expense.IsDeleted = true;
            _context.ExpensesFixeds.Update(expense);
            await _context.SaveChangesAsync();
            return Ok("Gasto archivado");
        }
    }
}
