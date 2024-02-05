using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesEventualController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public ExpensesEventualController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ExpensesEventual>> getExpenses()
        {
            var expenses = await _context.ExpensesEventuals
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(expenses);
        }
        [HttpGet("{ExpenseId}")]
        public async Task<ActionResult> getExpenseById(int ExpenseId)
        {
            var expense = await _context.ExpensesEventuals
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ExpenseId == ExpenseId);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }
        [HttpPost]
        public async Task<ActionResult<ExpensesEventual>> PostExpense(ExpensesEventual expense)
        {
            expense.CreatedDate = DateTime.Now;
            _context.ExpensesEventuals.Add(expense);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getExpenseById", new { ExpenseId = expense.ExpenseId }, expense);
        }
        [HttpPut("{ExpenseId}")]
        public async Task<ActionResult> PutExpense(int ExpenseId, ExpensesEventual expense)
        {
            if (expense.ExpenseId != ExpenseId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ExpenseOld = await _context.ExpensesEventuals
                .FirstOrDefaultAsync(f => f.ExpenseId == ExpenseId);
            if (ExpenseOld == null)
            {
                return BadRequest($"El cliente con el ID {ExpenseId} no existe");
            }
            ExpenseOld.ExpenseId = expense.ExpenseId;
            ExpenseOld.Name = expense.Name;
            ExpenseOld.Cost = expense.Cost;
            ExpenseOld.Description = expense.Description;
            ExpenseOld.IsDeleted = expense.IsDeleted;

            _context.ExpensesEventuals.Update(ExpenseOld);
            await _context.SaveChangesAsync();
            return Ok("El gasto se actualizo correctamente");
        }
        [HttpDelete("{ExpenseId}")]
        public async Task<IActionResult> DeleteExpense(int ExpenseId)
        {
            var expense = await _context.ExpensesEventuals
                .FirstOrDefaultAsync(f => f.ExpenseId == ExpenseId);
            if (expense == null)
            {
                return NotFound();
            }

            expense.IsDeleted = true;
            _context.ExpensesEventuals.Update(expense);
            await _context.SaveChangesAsync();
            return Ok("Gasto archivado");
        }
    }
}
