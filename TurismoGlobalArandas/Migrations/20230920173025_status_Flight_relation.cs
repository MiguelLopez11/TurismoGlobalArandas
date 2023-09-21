using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class status_Flight_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusFlightId",
                table: "ReservationFlights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_StatusFlightId",
                table: "ReservationFlights",
                column: "StatusFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlights_StatusFlight_StatusFlightId",
                table: "ReservationFlights",
                column: "StatusFlightId",
                principalTable: "StatusFlight",
                principalColumn: "StatusFlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlights_StatusFlight_StatusFlightId",
                table: "ReservationFlights");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlights_StatusFlightId",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "StatusFlightId",
                table: "ReservationFlights");
        }
    }
}
