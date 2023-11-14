using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservation_payment_flight_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationFlightId",
                table: "PaymentsRelationReservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountReceivedClient",
                table: "PaymentRelationLists",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountReturnedClient",
                table: "PaymentRelationLists",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethodClient",
                table: "PaymentRelationLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservations_ReservationFlightId",
                table: "PaymentsRelationReservations",
                column: "ReservationFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsRelationReservations_ReservationFlights_ReservationFlightId",
                table: "PaymentsRelationReservations",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsRelationReservations_ReservationFlights_ReservationFlightId",
                table: "PaymentsRelationReservations");

            migrationBuilder.DropIndex(
                name: "IX_PaymentsRelationReservations_ReservationFlightId",
                table: "PaymentsRelationReservations");

            migrationBuilder.DropColumn(
                name: "ReservationFlightId",
                table: "PaymentsRelationReservations");

            migrationBuilder.DropColumn(
                name: "AmountReceivedClient",
                table: "PaymentRelationLists");

            migrationBuilder.DropColumn(
                name: "AmountReturnedClient",
                table: "PaymentRelationLists");

            migrationBuilder.DropColumn(
                name: "PaymentMethodClient",
                table: "PaymentRelationLists");
        }
    }
}
