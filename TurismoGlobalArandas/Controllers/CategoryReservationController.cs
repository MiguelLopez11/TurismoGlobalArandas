using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryReservationController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public CategoryReservationController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryReservations>> getCategoryReservations()
        {
            var Categorys = await _context.CategoryReservations
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(Categorys);
        }
        [HttpGet("{CategoryReservationId}")]
        public async Task<ActionResult> getCategorysById(int CategoryReservationId)
        {
            var customer = await _context.CategoryReservations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.CategoryReservationId == CategoryReservationId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryReservations>> PostCategoryReservation(CategoryReservations categoryReservations)
        {
            _context.CategoryReservations.Add(categoryReservations);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getCategorysById", new { CategoryReservationId = categoryReservations.CategoryReservationId }, categoryReservations);
        }
        [HttpPut("{CategoryReservationId}")]
        public async Task<ActionResult> PutCategoryReservation(int CategoryReservationId, CategoryReservations categoryReservations)
        {
            if (categoryReservations.CategoryReservationId != CategoryReservationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var categoryOld = await _context.CategoryReservations
                .FirstOrDefaultAsync(f => f.CategoryReservationId == CategoryReservationId);
            if (categoryOld == null)
            {
                return BadRequest($"La categoría con el ID {CategoryReservationId} no existe");
            }
            categoryOld.CategoryReservationId = categoryReservations.CategoryReservationId;
            categoryOld.Name = categoryReservations.Name;
            categoryOld.Description = categoryReservations.Description;
            categoryOld.IsDeleted = categoryReservations.IsDeleted;

            _context.CategoryReservations.Update(categoryOld);
            await _context.SaveChangesAsync();
            return Ok("Categoría se actualizo correctamente");
        }
        [HttpDelete("{CategoryReservationId}")]
        public async Task<IActionResult> DeleteCategoryReservationId(int CategoryReservationId)
        {
            var category = await _context.CategoryReservations
                .FirstOrDefaultAsync(f => f.CategoryReservationId == CategoryReservationId);
            if (category == null)
            {
                return NotFound();
            }

            category.IsDeleted = true;
            _context.CategoryReservations.Update(category);
            await _context.SaveChangesAsync();
            return Ok("Categoría archivado");
        }
    }
}
