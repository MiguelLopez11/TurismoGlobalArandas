using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationsByEmployee_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
        CREATE PROCEDURE ReservationsByEmployee
    @EmployeeId INT
AS
BEGIN
    -- Lógica de tu procedimiento almacenado
    -- ...

    SELECT
        'Reservación hoteleria' AS Origin,
        ReservationHotels.TotalCost AS TotalCost,
        ReservationHotels.Invoice,
		ReservationHotels.EmployeeId,
        ReservationHotels.IsDeleted,
        ReservationHotels.IsSoldOut,
        ReservationHotels.DateSale
    FROM ReservationHotels
	JOIN Employees ON ReservationHotels.EmployeeId = Employees.EmployeeId
    WHERE Employees.EmployeeId = @EmployeeId

    UNION ALL

    SELECT
        'Reservación Tours' AS Origin,
        ReservationTours.NetPrice AS TotalCost,
        ReservationTours.Invoice,
		ReservationTours.EmployeeId,
        ReservationTours.IsDeleted,
        ReservationTours.IsSoldOut,
        ReservationTours.DateSale
    FROM ReservationTours
	JOIN Employees ON ReservationTours.EmployeeId = Employees.EmployeeId
    WHERE Employees.EmployeeId = @EmployeeId

    UNION ALL

    SELECT
        'Reservación Vuelos' AS Origin,
        ReservationFlights.PriceNeto AS TotalCost,
        ReservationFlights.Invoice,
		ReservationFlights.EmployeeId,
        ReservationFlights.IsDeleted,
        ReservationFlights.IsSoldOut,
        ReservationFlights.DateSale
    FROM ReservationFlights
	JOIN Employees ON ReservationFlights.EmployeeId = Employees.EmployeeId
    WHERE Employees.EmployeeId = @EmployeeId

    UNION ALL

    SELECT
        'Reservación Vehiculos' AS Origin,
        ReservationVehicles.PriceNeto AS TotalCost,
        ReservationVehicles.Invoice,
		ReservationVehicles.EmployeeId,
        ReservationVehicles.IsDeleted,
        ReservationVehicles.IsSoldOut,
        ReservationVehicles.DateSale
    FROM ReservationVehicles
	JOIN Employees ON ReservationVehicles.EmployeeId = Employees.EmployeeId
    WHERE Employees.EmployeeId = @EmployeeId
END

    "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS ReservationsByEmployee");
        }
    }
}
