using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationAditionalServices_Flight_vehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "ReservationHotelsServicesAditionals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationVehicleId",
                table: "ReservationHotelsServicesAditionals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelsServicesAditionals_ReservationFlightId",
                table: "ReservationHotelsServicesAditionals",
                column: "ReservationFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelsServicesAditionals_ReservationVehicleId",
                table: "ReservationHotelsServicesAditionals",
                column: "ReservationVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotelsServicesAditionals_ReservationFlights_ReservationFlightId",
                table: "ReservationHotelsServicesAditionals",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotelsServicesAditionals_ReservationVehicles_ReservationVehicleId",
                table: "ReservationHotelsServicesAditionals",
                column: "ReservationVehicleId",
                principalTable: "ReservationVehicles",
                principalColumn: "ReservationVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotelsServicesAditionals_ReservationFlights_ReservationFlightId",
                table: "ReservationHotelsServicesAditionals");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotelsServicesAditionals_ReservationVehicles_ReservationVehicleId",
                table: "ReservationHotelsServicesAditionals");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotelsServicesAditionals_ReservationFlightId",
                table: "ReservationHotelsServicesAditionals");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotelsServicesAditionals_ReservationVehicleId",
                table: "ReservationHotelsServicesAditionals");

            migrationBuilder.DropColumn(
                name: "ReservationFlightId",
                table: "ReservationHotelsServicesAditionals");

            migrationBuilder.DropColumn(
                name: "ReservationVehicleId",
                table: "ReservationHotelsServicesAditionals");

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

            migrationBuilder.UpdateData(
                table: "AditionalServices",
                keyColumn: "AditionalServiceId",
                keyValue: 1,
                columns: new[] { "ReservationFlightId", "ReservationVehicleId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AditionalServices",
                keyColumn: "AditionalServiceId",
                keyValue: 2,
                columns: new[] { "ReservationFlightId", "ReservationVehicleId" },
                values: new object[] { null, null });

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
    }
}
