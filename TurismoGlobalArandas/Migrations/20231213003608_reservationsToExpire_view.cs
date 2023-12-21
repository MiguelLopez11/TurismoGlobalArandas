using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationsToExpire_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW ReservationsToExpire
            AS
            SELECT 'Reservación hoteleria' AS Origin, Invoice
            FROM ReservationHotels
            WHERE DATEDIFF(day, GETDATE(), PaymentLimitDate) = 3

            UNION ALL

            SELECT 'Reservación Tours' AS Origin, Invoice
            FROM ReservationTours
            WHERE DATEDIFF(day, GETDATE(), PaymentLimitDate) = 3

            UNION ALL

            SELECT 'Reservación Vuelos' AS Origin, Invoice
            FROM ReservationFlights
            WHERE DATEDIFF(day, GETDATE(), PaymentLimitDate) = 3

            UNION ALL

            SELECT 'Reservación Vehiculos' AS Origin, Invoice
            FROM ReservationVehicles
            WHERE DATEDIFF(day, GETDATE(), PaymentLimitDate) = 3;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS ReservationsToExpire;");
        }
    }
}
