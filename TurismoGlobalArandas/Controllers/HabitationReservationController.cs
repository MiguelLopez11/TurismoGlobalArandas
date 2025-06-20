﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitationReservationController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public HabitationReservationController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<HabitationsReservation>> GetHabitationsReservations()
        {
            var habitationsReservations = await _context.HabitationsReservations
                .Include(i => i.Habitations)
                .Include(i => i.reservationHotel)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(habitationsReservations);
        }
        [HttpGet("ReservationHotel/{ReservationHotelId}")]
        public async Task<ActionResult<HabitationsReservation>> GetHabitationsReservationHotelId(int ReservationHotelId)
        {
            var habitationsReservations = await _context.HabitationsReservations
                .Include(i => i.Habitations)
                .Include(i => i.reservationHotel)
                .Where(w => w.ReservationHotelId == ReservationHotelId)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(habitationsReservations);
        }
        [HttpGet("{HabitationsReservationId}")]
        public async Task<ActionResult> GetHabitationReservationById(int HabitationsReservationId)
        {
            var habitationsReservation = await _context.HabitationsReservations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.HabitationsReservationId == HabitationsReservationId);
            if (habitationsReservation == null)
            {
                return NotFound();
            }
            return Ok(habitationsReservation);
        }
        [HttpPost]
        public async Task<ActionResult<HabitationsReservation>> PosthabitationsReservation(HabitationsReservation habitationsReservation)
        {
            _context.HabitationsReservations.Add(habitationsReservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction("gethabitationReservationById", new { HabitationsReservationId = habitationsReservation.HabitationsReservationId }, habitationsReservation);
        }
        [HttpPut("{HabitationsReservationId}")]
        public async Task<ActionResult> PutHabitation(int HabitationsReservationId, HabitationsReservation habitationsReservation)
        {
            if (habitationsReservation.HabitationsReservationId != HabitationsReservationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var habitationsReservationOld = await _context.HabitationsReservations
                .FirstOrDefaultAsync(f => f.HabitationsReservationId == HabitationsReservationId);

            if (habitationsReservationOld == null)
            {
                return BadRequest($"La habitacion con el ID {HabitationsReservationId} no existe");
            }
            habitationsReservationOld.HabitationId = habitationsReservation.HabitationId;
            habitationsReservationOld.HabitationsReservationId = habitationsReservation.HabitationsReservationId;
            habitationsReservationOld.IsDeleted = habitationsReservation.IsDeleted;

            _context.HabitationsReservations.Update(habitationsReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }
        [HttpDelete("{HabitationsReservationId}")]
        public async Task<IActionResult> DeleteHabitationReservation(int HabitationsReservationId)
        {
            var habitationsReservation = await _context.HabitationsReservations
                .FirstOrDefaultAsync(f => f.HabitationsReservationId == HabitationsReservationId);
            if (habitationsReservation == null)
            {
                return NotFound();
            }
            var habitation = await _context.Habitations
                .FirstOrDefaultAsync(f => f.HabitationId == habitationsReservation.HabitationId);
            habitation.IsDeleted = true;
            habitationsReservation.IsDeleted = true;
            _context.Habitations.Update(habitation);
            _context.HabitationsReservations.Update(habitationsReservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
