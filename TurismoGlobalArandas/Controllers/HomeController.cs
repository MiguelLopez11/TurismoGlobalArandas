using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;
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
                        r => new { Mes = r.DateSale.Value.Month, Tipo = "Reservación de hoteleria" }
                    )
                    .Concat(
                        _context.ReservationFlights
                            .Where(r => r.DateSale.Value.Year == DateTime.Now.Year)
                            .Where(w => !w.IsReservadedByHotel)
                            .Where(w => !w.IsDeleted)
                            .Select(
                                r =>
                                    new
                                    {
                                        Mes = r.DateSale.Value.Month,
                                        Tipo = "Reservación de vuelo"
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
        public ActionResult ObtenerDatosDesdeVista()
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
                let hotelReservations = _context.ReservationHotels
                    .Where(r => r.EmployeeId == employee.EmployeeId)
                    .ToList()
                let flightReservations = _context.ReservationFlights
                    .Where(r => r.EmployeeId == employee.EmployeeId)
                    .ToList()
                let vehicleReservations = _context.ReservationVehicles
                    .Where(r => r.EmployeeId == employee.EmployeeId)
                    .ToList()
                let tourReservations = _context.ReservationTours
                    .Where(r => r.EmployeeId == employee.EmployeeId)
                    .ToList()
                select new
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.Name,
                    EmployeeLastName = employee.Lastname,
                    TotalReservations = hotelReservations.Count
                        + flightReservations.Count
                        + vehicleReservations.Count
                        + tourReservations.Count
                }
            ).OrderByDescending(e => e.TotalReservations).FirstOrDefaultAsync();

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
                join destination in _context.Destinations
                    on reservation.DestinationId equals destination.DestinationId
                    into destinationsGroup
                from destination in destinationsGroup.DefaultIfEmpty() // Left join
                select new
                {
                    DestinationName = destination != null ? destination.Name : "Unknown",
                    ReservationCount = destination != null ? 1 : 0
                }
            )
                .GroupBy(x => new { x.DestinationName })
                .Select(
                    group =>
                        new
                        {
                            Destination = group.Key.DestinationName,
                            ReservationsCount = group.Sum(x => x.ReservationCount)
                        }
                )
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

            var reservationsByEmployee = employees
                .Select(employee =>
                {
                    var hotelReservationsCount = _context.ReservationHotels.Count(
                        r => r.EmployeeId == employee.EmployeeId
                    );

                    var flightReservationsCount = _context.ReservationFlights.Count(
                        r => r.EmployeeId == employee.EmployeeId
                    );

                    var vehicleReservationsCount = _context.ReservationVehicles.Count(
                        r => r.EmployeeId == employee.EmployeeId
                    );

                    var tourReservationsCount = _context.ReservationTours.Count(
                        r => r.EmployeeId == employee.EmployeeId
                    );

                    return new
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.Name + " " + employee.Lastname,
                        HotelReservationsCount = hotelReservationsCount,
                        FlightReservationsCount = flightReservationsCount,
                        VehicleReservationsCount = vehicleReservationsCount,
                        TourReservationsCount = tourReservationsCount,
                        TotalReservationsCount = hotelReservationsCount
                            + flightReservationsCount
                            + vehicleReservationsCount
                            + tourReservationsCount
                    };
                })
                .ToList();

            return Ok(reservationsByEmployee);
        }

        [HttpGet("ReservationsCount")]
        public async Task<ActionResult> GetReservationsCount()
        {
            var reservaciones = (
                await _context.ReservationHotels
                    .Where(w => !w.IsDeleted)
                    .Select(r => new { Tipo = "Reservación de hoteleria" })
                    .Concat(
                        _context.ReservationFlights
                            .Where(w => !w.IsReservadedByHotel)
                            .Where(w => !w.IsDeleted)
                            .Select(r => new { Tipo = "Reservación de vuelo" })
                    )
                    .Concat(
                        _context.ReservationVehicles
                            .Where(w => !w.IsDeleted)
                            .Select(r => new { Tipo = "Reservación de Vehiculos" })
                    )
                    .Concat(
                        _context.ReservationTours
                            .Where(w => !w.IsDeleted)
                            .Select(r => new { Tipo = "Reservación de Tours" })
                    )
                    .ToListAsync()
            )
                .GroupBy(r => new { r.Tipo })
                .Select(group => new { Tipo = group.Key.Tipo, Cantidad = group.Count() })
                .ToArray();

            return Ok(reservaciones);
        }

        [HttpPost("CommissionsEmployee")]
        public async Task<ActionResult> GetCommissionsEmployee([FromBody] List<DateTime> dates)
        {
            List<CommisionsEmployee> commisions = new List<CommisionsEmployee>();
            #region reservacion hoteleria individual
            var individualRates = _context.IndividualRates
                .Include(i => i.reservationHotel)
                .AsEnumerable()
                .Where(
                            r =>
                                dates == null
                                || dates.Count == 0
                                || (
                                    dates.Any(date => r.reservationHotel.DateSale == date.Date)
                                    || (
                                        r.reservationHotel.DateSale >= dates[0].Date
                                        && r.reservationHotel.DateSale <= dates[dates.Count - 1].Date
                                    )
                                )
                        )
                .Where(w => !w.IsDeleted)
                .ToList();
            foreach (var item in individualRates)
            {
                var ReservationHotel = await _context.ReservationHotels
                    .Include(i => i.Providers)
                    .FirstOrDefaultAsync(f => f.ReservationHotelId == item.ReservationHotelId);
                var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                    f => f.ProviderId == ReservationHotel.ProviderId
                );
                var employee = await _context.Employees.FirstOrDefaultAsync(
                    f => f.EmployeeId == ReservationHotel.EmployeeId
                );
                if (item.PublicRate != null)
                {
                    decimal RevenueHotel = 0;
                    var commisionAgency =
                        (decimal)item.PublicRate * (service.CommissionAgency / 100);
                    var commisionClient =
                        (decimal)item.PublicRate * (service.CommissionClient / 100);
                    commisionAgency = (decimal)item.PublicRate - commisionAgency;
                    commisionClient = (decimal)item.PublicRate - commisionClient;
                    RevenueHotel += (commisionAgency - commisionClient) * -1;
                    var commisionEmployee = RevenueHotel * (service.CommissionEmployee / 100);
                    commisions.Add(
                        new CommisionsEmployee
                        {
                            Commission = commisionEmployee,
                            EmployeeName = employee.Name + " " + employee.Lastname,
                            InvoiceReservation = ReservationHotel.Invoice,
                            DateSale = ReservationHotel.DateSale,
                            TypeReservation = "Reservación hoteleria individual"
                        }
                    );
                }
            }
            #endregion
            #region reservacion hoteleria grupal
            var GroupRate = _context.GroupRates
                .Include(i => i.ReservationHotelGroup)
                .AsEnumerable()
                .Where(
                            r =>
                                dates == null
                                || dates.Count == 0
                                || (
                                    dates.Any(date => r.DateSale == date.Date)
                                    || (
                                        r.DateSale >= dates[0].Date
                                        && r.DateSale <= dates[dates.Count - 1].Date
                                    )
                                )
                        )
                .Where(w => !w.IsDeleted)
                .ToList();
            foreach (var item in GroupRate)
            {
                if (item.RangeTotal != null)
                {
                    decimal? RevenueGroupRate = 0;
                    var ReservationHotel = await _context.ReservationHotels
                        .Include(i => i.Providers)
                        .FirstOrDefaultAsync(
                            f =>
                                f.ReservationHotelId
                                == item.ReservationHotelGroup.ReservationHotelId
                        );
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == ReservationHotel.ProviderId
                    );
                    var employee = await _context.Employees.FirstOrDefaultAsync(
                    f => f.EmployeeId == ReservationHotel.EmployeeId
                    );
                    RevenueGroupRate += (item.RangeTotal - item.RangeAgency);
                    var commisionEmployee = RevenueGroupRate * (service.CommissionEmployee / 100);
                    commisions.Add(
                        new CommisionsEmployee
                        {
                            Commission = commisionEmployee,
                            EmployeeName = employee.Name + " " + employee.Lastname,
                            InvoiceReservation = ReservationHotel.Invoice,
                            DateSale = ReservationHotel.DateSale,
                            TypeReservation = "Reservación hoteleria grupal"
                        }
                    );
                }
            }
            #endregion
            #region Flights
            var reservationFlights = _context.ReservationFlights
                .AsEnumerable()
                .Where(
                            r =>
                                dates == null
                                || dates.Count == 0
                                || (
                                    dates.Any(date => r.DateSale == date.Date)
                                    || (
                                        r.DateSale >= dates[0].Date
                                        && r.DateSale <= dates[dates.Count - 1].Date
                                    )
                                )
                        )
                .Where(w => !w.IsDeleted)
                .ToList();
            foreach (var item in reservationFlights)
            {
                if (item.PriceNeto != null)
                {
                    decimal? RevenueFlight = 0;
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == item.ProviderId
                    );
                    var employee = await _context.Employees.FirstOrDefaultAsync(
                    f => f.EmployeeId == item.EmployeeId
                    );
                    RevenueFlight += (item.PriceNeto - item.PricePublic);
                    var commisionEmployee = RevenueFlight * (service.CommissionEmployee / 100);
                    commisions.Add(
                        new CommisionsEmployee
                        {
                            Commission = commisionEmployee,
                            EmployeeName = employee.Name + " " + employee.Lastname,
                            InvoiceReservation = item.Invoice,
                            DateSale = item.DateSale,
                            TypeReservation = "Reservación Vuelos"
                        }
                    );
                }
            }
            #endregion
            #region Vehicles
            var reservationVehicles = _context.ReservationVehicles
                .AsEnumerable()
                .Where(
                            r =>
                                dates == null
                                || dates.Count == 0
                                || (
                                    dates.Any(date => r.DateSale == date.Date)
                                    || (
                                        r.DateSale >= dates[0].Date
                                        && r.DateSale <= dates[dates.Count - 1].Date
                                    )
                                )
                        )
                .Where(w => !w.IsDeleted)
                .ToList();
            foreach (var item in reservationVehicles)
            {
                if (item.PriceNeto != null)
                {
                    decimal? RevenueVehicle = 0;
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == item.ProviderId
                    );
                    var employee = await _context.Employees.FirstOrDefaultAsync(
                    f => f.EmployeeId == item.EmployeeId
                    );
                    RevenueVehicle += (item.PriceNeto - item.PricePublic);
                    var commisionEmployee = RevenueVehicle * (service.CommissionEmployee / 100);
                    commisions.Add(
                        new CommisionsEmployee
                        {
                            Commission = commisionEmployee,
                            EmployeeName = employee.Name + employee.Lastname,
                            InvoiceReservation = item.Invoice,
                            DateSale = item.DateSale,
                            TypeReservation = "Reservación Vuelos"
                        }
                    );
                }
            }
            #endregion
            #region Tours
            var reservationTours = _context.ReservationTours
                .AsEnumerable()
                .Where(
                            r =>
                                dates == null
                                || dates.Count == 0
                                || (
                                    dates.Any(date => r.DateSale == date.Date)
                                    || (
                                        r.DateSale >= dates[0].Date
                                        && r.DateSale <= dates[dates.Count - 1].Date
                                    )
                                )
                        )
                .Where(w => !w.IsDeleted)
                .ToList();
            foreach (var item in reservationTours)
            {
                if (item.NetPrice != null)
                {
                    decimal? RevenueVehicle = 0;
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == item.ProviderId
                    );
                    var employee = await _context.Employees.FirstOrDefaultAsync(
                    f => f.EmployeeId == item.EmployeeId
                    );
                    RevenueVehicle += (item.NetPrice - item.PublicRate);
                    var commisionEmployee = RevenueVehicle * (service.CommissionEmployee / 100);
                    commisions.Add(
                        new CommisionsEmployee
                        {
                            Commission = commisionEmployee,
                            EmployeeName = employee.Name + " " + employee.Lastname,
                            InvoiceReservation = item.Invoice,
                            DateSale = item.DateSale,
                            TypeReservation = "Reservación Tours"
                        }
                    );
                }
            }
            #endregion
            return Ok(commisions);
        }

        [HttpPost("TotalRevenue")]
        public async Task<ActionResult> GetTotalRevenue()
        {
            decimal? Revenue = 0;
            #region reservacion hoteleria individual
            var individualRates = await _context.IndividualRates
                .Include(i => i.reservationHotel)
                .Where(r => r.reservationHotel.DateSale.Value.Month == DateTime.Now.Month)
                .Where(w => !w.IsDeleted)
                .ToListAsync();

            var ExpensesEventual = await _context.ExpensesEventuals
                .Where(r => r.CreatedDate.Month == DateTime.Now.Month)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            var ExpensesFixed = await _context.ExpensesFixeds
                .Where(r => r.CreatedDate.Month == DateTime.Now.Month)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            foreach (var item in individualRates)
            {
                var ReservationHotel = await _context.ReservationHotels
                    .Include(i => i.Providers)
                    .FirstOrDefaultAsync(f => f.ReservationHotelId == item.ReservationHotelId);
                var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                    f => f.ProviderId == ReservationHotel.ProviderId
                );
                if (item.PublicRate != null)
                {
                    decimal RevenueHotel = 0;
                    var commisionAgency =
                        (decimal)item.PublicRate * (service.CommissionAgency / 100);
                    var commisionClient =
                        (decimal)item.PublicRate * (service.CommissionClient / 100);
                    commisionAgency = (decimal)item.PublicRate - commisionAgency;
                    commisionClient = (decimal)item.PublicRate - commisionClient;
                    RevenueHotel += (commisionAgency - commisionClient) * -1;
                    var commisionEmployee = RevenueHotel * (service.CommissionEmployee / 100);
                    Revenue += RevenueHotel - commisionEmployee;
                }
            }
            #endregion
            #region reservacion hoteleria grupal
            var GroupRate = await _context.GroupRates
                .Include(i => i.ReservationHotelGroup)
                .Where(r => r.DateSale.Month == DateTime.Now.Month)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            foreach (var item in GroupRate)
            {
                if (item.RangeTotal != null)
                {
                    decimal? RevenueGroupRate = 0;
                    var ReservationHotel = await _context.ReservationHotels
                        .Include(i => i.Providers)
                        .FirstOrDefaultAsync(
                            f =>
                                f.ReservationHotelId
                                == item.ReservationHotelGroup.ReservationHotelId
                        );
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == ReservationHotel.ProviderId
                    );
                    RevenueGroupRate += (item.RangeTotal - item.RangeAgency);
                    var commisionEmployee = RevenueGroupRate * (service.CommissionEmployee / 100);
                    Revenue += RevenueGroupRate - commisionEmployee;
                }
            }
            #endregion
            #region Flights
            var reservationFlights = await _context.ReservationFlights
                .Where(r => r.DateSale.Value.Month == DateTime.Now.Month)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            foreach (var item in reservationFlights)
            {
                if (item.PriceNeto != null)
                {
                    decimal? RevenueFlight = 0;
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == item.ProviderId
                    );
                    RevenueFlight += (item.PriceNeto - item.PricePublic);
                    var commisionEmployee = RevenueFlight * (service.CommissionEmployee / 100);
                    Revenue += RevenueFlight - commisionEmployee;
                }
            }
            #endregion
            #region Vehicles
            var reservationVehicles = await _context.ReservationVehicles
                .Where(r => r.DateSale.Value.Month == DateTime.Now.Month)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            foreach (var item in reservationVehicles)
            {
                if (item.PriceNeto != null)
                {
                    decimal? RevenueVehicle = 0;
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == item.ProviderId
                    );
                    RevenueVehicle += (item.PriceNeto - item.PricePublic);
                    var commisionEmployee = RevenueVehicle * (service.CommissionEmployee / 100);
                    Revenue += RevenueVehicle - commisionEmployee;
                }
            }
            #endregion
            #region Tours
            var reservationTours = await _context.ReservationTours
                .Where(r => r.DateSale.Value.Month == DateTime.Now.Month)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            foreach (var item in reservationTours)
            {
                if (item.NetPrice != null)
                {
                    decimal? RevenueVehicle = 0;
                    var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                        f => f.ProviderId == item.ProviderId
                    );
                    RevenueVehicle += (item.NetPrice - item.PublicRate);
                    var commisionEmployee = RevenueVehicle * (service.CommissionEmployee / 100);
                    Revenue += RevenueVehicle - commisionEmployee;
                }
            }
            foreach (var item in ExpensesEventual)
            {
                Revenue -= item.Cost;
            }
            foreach (var item in ExpensesFixed)
            {
                Revenue -= item.Cost;
            }
            #endregion
            return Ok(Revenue);
        }

        [HttpGet("ReservationCount")]
        public async Task<ActionResult> GetCountReservations()
        {
            var mostPopularDestination = await (
                from reservation in _context.ReservationHotels
                where reservation.DateSale.Value.Year == DateTime.Now.Year
                join destination in _context.Destinations
                    on reservation.DestinationId equals destination.DestinationId
                    into destinationsGroup
                from destination in destinationsGroup.DefaultIfEmpty() // Left join
                select new
                {
                    DestinationName = destination != null ? destination.Name : "Unknown",
                    ReservationCount = destination != null ? 1 : 0
                }
            )
                .GroupBy(x => new { x.DestinationName })
                .Select(
                    group =>
                        new
                        {
                            Destination = group.Key.DestinationName,
                            ReservationsCount = group.Sum(x => x.ReservationCount)
                        }
                )
                .OrderByDescending(x => x.ReservationsCount)
                .FirstOrDefaultAsync();

            if (mostPopularDestination == null)
            {
                return NotFound("No se encontraron reservaciones de hotelería");
            }

            return Ok(mostPopularDestination);
        }
    }
}
