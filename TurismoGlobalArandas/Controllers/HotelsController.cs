using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public HotelsController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Hotels>> getHotels()
        {
            var hotels = await _context.Hotels
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(hotels);
        }
        [HttpGet("{HotelId}")]
        public async Task<ActionResult> getHotelById(int HotelId)
        {
            var hotel = await _context.Hotels
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.HotelId == HotelId);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }
        [HttpPost]
        public async Task<ActionResult<Hotels>> PostHotel(Hotels hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getHotelById", new { HotelId = hotel.HotelId }, hotel );
        }
        [HttpPut("{HotelId}")]
        public async Task<ActionResult> PutHotel(int HotelId, Hotels hotel)
        {
            if (hotel.HotelId != HotelId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var hotelOld = await _context.Hotels
                .FirstOrDefaultAsync(f => f.HotelId == HotelId);

            if (hotelOld == null)
            {
                return BadRequest($"La habitacion con el ID {HotelId} no existe");
            }
            hotelOld.HotelId = hotel.DestinationId;
            hotelOld.Name = hotel.Name;
            hotelOld.DestinationId = hotel.DestinationId;
            hotelOld.Description = hotel.Description;
            hotelOld.IsDeleted = hotel.IsDeleted;

            _context.Hotels.Update(hotelOld);
            await _context.SaveChangesAsync();
            return Ok("Hotel actualizado correctamente");
        }
        [HttpDelete("{HotelId}")]
        public async Task<IActionResult> DeleteHotel(int HotelId)
        {
            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(f => f.HotelId == HotelId);
            if (hotel == null)
            {
                return NotFound();
            }

            hotel.IsDeleted = true;
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
            return Ok("hotel archivado");
        }
    }
}
