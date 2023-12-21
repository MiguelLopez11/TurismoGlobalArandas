using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Reservation_flight_destination_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationFlightId",
                table: "ReservationFlightDestinations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlightDestinations_ReservationFlightId",
                table: "ReservationFlightDestinations",
                column: "ReservationFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlightDestinations_ReservationFlights_ReservationFlightId",
                table: "ReservationFlightDestinations",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlightDestinations_ReservationFlights_ReservationFlightId",
                table: "ReservationFlightDestinations");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlightDestinations_ReservationFlightId",
                table: "ReservationFlightDestinations");

            migrationBuilder.DropColumn(
                name: "ReservationFlightId",
                table: "ReservationFlightDestinations");
        }
    }
}
