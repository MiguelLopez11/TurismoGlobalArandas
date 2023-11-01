using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class serviceAditional_Flight_vehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_ReservationFlights_ReservationFlightId",
                table: "ReservationHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_ReservationVehicles_ReservationVehicleId",
                table: "ReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_ReservationFlightId",
                table: "ReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_ReservationVehicleId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "ReservationFlightId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "ReservationVehicleId",
                table: "ReservationHotels");

            migrationBuilder.AddColumn<int>(
                name: "ReservationFlightId",
                table: "AditionalServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationVehicleId",
                table: "AditionalServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AditionalServices_ReservationFlightId",
                table: "AditionalServices",
                column: "ReservationFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_AditionalServices_ReservationVehicleId",
                table: "AditionalServices",
                column: "ReservationVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AditionalServices_ReservationFlights_ReservationFlightId",
                table: "AditionalServices",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_AditionalServices_ReservationVehicles_ReservationVehicleId",
                table: "AditionalServices",
                column: "ReservationVehicleId",
                principalTable: "ReservationVehicles",
                principalColumn: "ReservationVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AditionalServices_ReservationFlights_ReservationFlightId",
                table: "AditionalServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AditionalServices_ReservationVehicles_ReservationVehicleId",
                table: "AditionalServices");

            migrationBuilder.DropIndex(
                name: "IX_AditionalServices_ReservationFlightId",
                table: "AditionalServices");

            migrationBuilder.DropIndex(
                name: "IX_AditionalServices_ReservationVehicleId",
                table: "AditionalServices");

            migrationBuilder.DropColumn(
                name: "ReservationFlightId",
                table: "AditionalServices");

            migrationBuilder.DropColumn(
                name: "ReservationVehicleId",
                table: "AditionalServices");

            migrationBuilder.AddColumn<int>(
                name: "ReservationFlightId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationVehicleId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_ReservationFlightId",
                table: "ReservationHotels",
                column: "ReservationFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_ReservationVehicleId",
                table: "ReservationHotels",
                column: "ReservationVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_ReservationFlights_ReservationFlightId",
                table: "ReservationHotels",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_ReservationVehicles_ReservationVehicleId",
                table: "ReservationHotels",
                column: "ReservationVehicleId",
                principalTable: "ReservationVehicles",
                principalColumn: "ReservationVehicleId");
        }
    }
}
