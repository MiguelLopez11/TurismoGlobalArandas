using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class getAllReservations_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
        CREATE VIEW Get_All_Reservations AS
        SELECT 'Reservación hoteleria' AS Origin, 
       ReservationHotels.TotalCost AS TotalCost,
	   ReservationHotels.ReservationHotelId AS ReservationId,
       ReservationHotels.Invoice, 
       ReservationHotels.EmployeeId, 
       ReservationHotels.IsDeleted, 
       ReservationHotels.IsSoldOut, 
       ReservationHotels.DateSale,
	   Destinations.Name AS Destino,
       Customers.Name AS Name,
       Customers.LastName AS LastName,
       Customers.PhoneNumber AS PhoneNumber
FROM ReservationHotels 
JOIN Employees ON ReservationHotels.EmployeeId = Employees.EmployeeId
JOIN Customers ON ReservationHotels.CustomerId = Customers.CustomerId
JOIN Destinations ON ReservationHotels.DestinationId = Destinations.DestinationId

UNION ALL

SELECT 'Reservación Tours' AS Origin, 
       ReservationTours.NetPrice AS TotalCost,
	   ReservationTours.ReservationTourId AS ReservationId,
       ReservationTours.Invoice, 
       ReservationTours.EmployeeId, 
       ReservationTours.IsDeleted, 
       ReservationTours.IsSoldOut, 
       ReservationTours.DateSale,
	   Destinations.Name AS Destino,
       Customers.Name AS Name,
       Customers.LastName AS LastName,
       Customers.PhoneNumber AS PhoneNumber
FROM ReservationTours 
JOIN Employees ON ReservationTours.EmployeeId = Employees.EmployeeId
JOIN Customers ON ReservationTours.CustomerId = Customers.CustomerId
JOIN Destinations ON ReservationTours.DestinationId = Destinations.DestinationId

UNION ALL

SELECT 'Reservación Vuelos' AS Origin, 
       ReservationFlights.PriceNeto AS TotalCost,
	   ReservationFlights.FlightId AS ReservationId,
       ReservationFlights.Invoice, 
       ReservationFlights.EmployeeId, 
       ReservationFlights.IsDeleted, 
       ReservationFlights.IsSoldOut, 
       ReservationFlights.DateSale,
	   Routes.RouteName AS Destino,
       Customers.Name AS Name,
       Customers.LastName AS LastName,
       Customers.PhoneNumber AS PhoneNumber
FROM ReservationFlights 
JOIN Employees ON ReservationFlights.EmployeeId = Employees.EmployeeId
JOIN Customers ON ReservationFlights.CustomerId = Customers.CustomerId
JOIN Routes ON ReservationFlights.RouteArrivalAirportId = Routes.RouteId

UNION ALL

SELECT 'Reservación Vehiculos' AS Origin, 
       ReservationVehicles.PriceNeto AS TotalCost,
	   ReservationVehicles.ReservationVehicleId AS ReservationId,
       ReservationVehicles.Invoice, 
       ReservationVehicles.EmployeeId, 
       ReservationVehicles.IsDeleted, 
       ReservationVehicles.IsSoldOut, 
       ReservationVehicles.DateSale,
	   ReservationVehicles.ArrivalLocation AS Destino,
        Customers.Name AS Name,
       Customers.LastName AS LastName,
       Customers.PhoneNumber AS PhoneNumber
FROM ReservationVehicles 
JOIN Employees ON ReservationVehicles.EmployeeId = Employees.EmployeeId
JOIN Customers ON ReservationVehicles.CustomerId = Customers.CustomerId
    "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS All_Reservations");
        }
    }
}
