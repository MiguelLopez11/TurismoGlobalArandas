using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models.Identity;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public HomeController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet("ReservationsByMonth")]
        public async Task<ActionResult> getReservationHotelByMonth()
        {
            var reservacionesPorMes = (
                await _context.ReservationHotels
                    .Where(r => r.DateSale.Value.Year == DateTime.Now.Year)
                    .Where(w => !w.IsDeleted)
                    .Select(
                        r => new { Mes = r.DateSale.Value.Month, Tipo = "Reservación de vuelo" }
                    )
                    .Concat(
                        _context.ReservationFlights
                            .Where(r => r.DateSale.Value.Year == DateTime.Now.Year)
                            .Where(w => !w.IsDeleted)
                            .Select(
                                r =>
                                    new
                                    {
                                        Mes = r.DateSale.Value.Month,
                                        Tipo = "Reservación de Hotelería"
                                    }
                            )
                    )
                    .Concat(
                        _context.ReservationVehicles
                            .Where(r => r.DateSale.Value.Year == DateTime.Now.Year)
                            .Where(w => !w.IsDeleted)
                            .Select(
                                r =>
                                    new
                                    {
                                        Mes = r.DateSale.Value.Month,
                                        Tipo = "Reservación de Vehiculos"
                                    }
                            )
                    )
                    .Concat(
                        _context.ReservationTours
                            .Where(r => r.DateSale.Value.Year == DateTime.Now.Year)
                            .Where(w => !w.IsDeleted)
                            .Select(
                                r =>
                                    new
                                    {
                                        Mes = r.DateSale.Value.Month,
                                        Tipo = "Reservación de Tours"
                                    }
                            )
                    )
                    .ToListAsync()
            )
                .GroupBy(r => new { r.Mes, r.Tipo })
                .Select(
                    group =>
                        new
                        {
                            Cantidad = group.Count()
                        }
                )
                .ToArray();

            return Ok(reservacionesPorMes);
        }
        [HttpPost("AllReservations")]
        public  ActionResult ObtenerDatosDesdeVista(
        )
        {
                var datosVista =  _context.GetAllReservationsViews
                    .FromSqlRaw("SELECT * FROM Get_All_Reservations")
                    .AsEnumerable()
                    .ToList();

                return Ok(datosVista);
            
        }



    }
}

