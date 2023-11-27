using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class paymentProvider_nulleables_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationFlights_ReservationFlightId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationHotels_ReservationHotelId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationTours_ReservationTourId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationVehicles_ReservationVehicleId",
                table: "PaymentProviders");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationVehicleId",
                table: "PaymentProviders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationTourId",
                table: "PaymentProviders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationHotelId",
                table: "PaymentProviders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationFlightId",
                table: "PaymentProviders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationFlights_ReservationFlightId",
                table: "PaymentProviders",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationHotels_ReservationHotelId",
                table: "PaymentProviders",
                column: "ReservationHotelId",
                principalTable: "ReservationHotels",
                principalColumn: "ReservationHotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationTours_ReservationTourId",
                table: "PaymentProviders",
                column: "ReservationTourId",
                principalTable: "ReservationTours",
                principalColumn: "ReservationTourId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationVehicles_ReservationVehicleId",
                table: "PaymentProviders",
                column: "ReservationVehicleId",
                principalTable: "ReservationVehicles",
                principalColumn: "ReservationVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationFlights_ReservationFlightId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationHotels_ReservationHotelId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationTours_ReservationTourId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_ReservationVehicles_ReservationVehicleId",
                table: "PaymentProviders");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationVehicleId",
                table: "PaymentProviders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationTourId",
                table: "PaymentProviders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationHotelId",
                table: "PaymentProviders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationFlightId",
                table: "PaymentProviders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationFlights_ReservationFlightId",
                table: "PaymentProviders",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationHotels_ReservationHotelId",
                table: "PaymentProviders",
                column: "ReservationHotelId",
                principalTable: "ReservationHotels",
                principalColumn: "ReservationHotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationTours_ReservationTourId",
                table: "PaymentProviders",
                column: "ReservationTourId",
                principalTable: "ReservationTours",
                principalColumn: "ReservationTourId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_ReservationVehicles_ReservationVehicleId",
                table: "PaymentProviders",
                column: "ReservationVehicleId",
                principalTable: "ReservationVehicles",
                principalColumn: "ReservationVehicleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
