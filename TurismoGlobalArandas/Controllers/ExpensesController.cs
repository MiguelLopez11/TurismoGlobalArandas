using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public ExpensesController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Expenses>> getExpenses()
        {
            var expenses = await _context.Expenses
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(expenses);
        }
        [HttpGet("{ExpenseId}")]
        public async Task<ActionResult> getExpenseById(int ExpenseId)
        {
            var expense = await _context.Expenses
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ExpenseId == ExpenseId);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }
        [HttpPost]
        public async Task<ActionResult<Expenses>> PostExpense(Expenses expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getExpenseById", new { ExpenseId = expense.ExpenseId }, expense);
        }
        [HttpPut("{ExpenseId}")]
        public async Task<ActionResult> PutExpense(int ExpenseId, Expenses expense)
        {
            if (expense.ExpenseId != ExpenseId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ExpenseOld = await _context.Expenses
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

            _context.Expenses.Update(ExpenseOld);
            await _context.SaveChangesAsync();
            return Ok("El gasto se actualizo correctamente");
        }
        [HttpDelete("{ExpenseId}")]
        public async Task<IActionResult> DeleteExpense(int ExpenseId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(f => f.ExpenseId == ExpenseId);
            if (expense == null)
            {
                return NotFound();
            }

            expense.IsDeleted = true;
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return Ok("Gasto archivado");
        }
    }
}
