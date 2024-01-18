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
        public async Task<ActionResult> GetReservationHotelByMonth()
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
                            Mes = group.Key.Mes,
                            Tipo = group.Key.Tipo,
                            Cantidad = group.Count()
                        }
                )
                .ToArray();

            // Crear un array de 12 elementos con valores iniciales en 0
            int[] cantidadesPorMes = new int[12];

            // Llenar el array con las cantidades obtenidas
            foreach (var item in reservacionesPorMes)
            {
                cantidadesPorMes[item.Mes - 1] += item.Cantidad;
            }

            return Ok(cantidadesPorMes);
        }

        [HttpPost("AllReservations")]
        public ActionResult ObtenerDatosDesdeVista(
        )
        {
            var datosVista = _context.GetAllReservationsViews
                .FromSqlRaw("SELECT * FROM Get_All_Reservations")
                .AsEnumerable()
                .ToList();

            return Ok(datosVista);

        }
        [HttpGet("EmployeeWithMostReservations")]
        public async Task<ActionResult> GetEmployeeWithMostReservations()
        {
            var employeeWithMostReservations = await (
                from employee in _context.Employees
                let hotelReservations = _context.ReservationHotels.Where(r => r.EmployeeId == employee.EmployeeId).ToList()
                let flightReservations = _context.ReservationFlights.Where(r => r.EmployeeId == employee.EmployeeId).ToList()
                let vehicleReservations = _context.ReservationVehicles.Where(r => r.EmployeeId == employee.EmployeeId).ToList()
                let tourReservations = _context.ReservationTours.Where(r => r.EmployeeId == employee.EmployeeId).ToList()
                select new
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.Name,
                    EmployeeLastName = employee.Lastname,
                    TotalReservations = hotelReservations.Count + flightReservations.Count + vehicleReservations.Count + tourReservations.Count
                })
                .OrderByDescending(e => e.TotalReservations)
                .FirstOrDefaultAsync();

            if (employeeWithMostReservations == null)
            {
                return NotFound("No se encontró ningún empleado con reservaciones");
            }

            return Ok(employeeWithMostReservations);
        }
        [HttpGet("MostPopularDestination")]
        public async Task<ActionResult> GetMostPopularDestination()
        {
            var mostPopularDestination = await (
                from reservation in _context.ReservationHotels
                where reservation.DateSale.Value.Year == DateTime.Now.Year
                join destination in _context.Destinations on reservation.DestinationId equals destination.DestinationId into destinationsGroup
                from destination in destinationsGroup.DefaultIfEmpty() // Left join
                select new
                {
                    DestinationName = destination != null ? destination.Name : "Unknown",
                    ReservationCount = destination != null ? 1 : 0
                })
                .GroupBy(x => new { x.DestinationName })
                .Select(group => new
                {
                    Destination = group.Key.DestinationName,
                    ReservationsCount = group.Sum(x => x.ReservationCount)
                })
                .OrderByDescending(x => x.ReservationsCount)
                .FirstOrDefaultAsync();

            if (mostPopularDestination == null)
            {
                return NotFound("No se encontraron reservaciones de hotelería");
            }

            return Ok(mostPopularDestination);
        }
        [HttpGet("ReservationsByEmployee")]
        public async Task<ActionResult> GetReservationsByEmployee()
        {
            var employees = await _context.Employees.ToListAsync();

            var reservationsByEmployee = employees.Select(employee =>
            {
                var hotelReservationsCount = _context.ReservationHotels
                    .Count(r => r.EmployeeId == employee.EmployeeId);

                var flightReservationsCount = _context.ReservationFlights
                    .Count(r => r.EmployeeId == employee.EmployeeId);

                var vehicleReservationsCount = _context.ReservationVehicles
                    .Count(r => r.EmployeeId == employee.EmployeeId);

                var tourReservationsCount = _context.ReservationTours
                    .Count(r => r.EmployeeId == employee.EmployeeId);

                return new
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.Name,
                    HotelReservationsCount = hotelReservationsCount,
                    FlightReservationsCount = flightReservationsCount,
                    VehicleReservationsCount = vehicleReservationsCount,
                    TourReservationsCount = tourReservationsCount,
                    TotalReservationsCount = hotelReservationsCount + flightReservationsCount + vehicleReservationsCount + tourReservationsCount
                };
            }).ToList();

            return Ok(reservationsByEmployee);
        }

        [HttpGet("TotalRevenue")]
        public async Task<ActionResult> GetTotalRevenue([FromQuery] List<DateTime> dateRange)
        {
            if (dateRange == null || dateRange.Count != 2)
            {
                return BadRequest("Se requiere una lista de dos fechas.");
            }

            DateTime startDate = dateRange[0];
            DateTime endDate = dateRange[1];

            if (startDate > endDate)
            {
                return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            var totalRevenue = await (
                from hotelReservation in _context.ReservationHotels
                where hotelReservation.DateSale >= startDate && hotelReservation.DateSale <= endDate
                select hotelReservation.TotalCost
            )
            .Concat(
                from flightReservation in _context.ReservationFlights
                where flightReservation.DateSale >= startDate && flightReservation.DateSale <= endDate
                select flightReservation.PriceNeto
            )
            .Concat(
                from vehicleReservation in _context.ReservationVehicles
                where vehicleReservation.DateSale >= startDate && vehicleReservation.DateSale <= endDate
                select vehicleReservation.PriceNeto
            )
            .Concat(
                from tourReservation in _context.ReservationTours
                where tourReservation.DateSale >= startDate && tourReservation.DateSale <= endDate
                select tourReservation.NetPrice
            )
            .SumAsync();

            return Ok(totalRevenue);
        }




    }
}

