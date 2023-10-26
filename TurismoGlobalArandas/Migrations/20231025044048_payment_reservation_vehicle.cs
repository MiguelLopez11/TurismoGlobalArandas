using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class payment_reservation_vehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationVehicleId",
                table: "PaymentsRelationReservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservations_ReservationVehicleId",
                table: "PaymentsRelationReservations",
                column: "ReservationVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsRelationReservations_ReservationVehicles_ReservationVehicleId",
                table: "PaymentsRelationReservations",
                column: "ReservationVehicleId",
                principalTable: "ReservationVehicles",
                principalColumn: "ReservationVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsRelationReservations_ReservationVehicles_ReservationVehicleId",
                table: "PaymentsRelationReservations");

            migrationBuilder.DropIndex(
                name: "IX_PaymentsRelationReservations_ReservationVehicleId",
                table: "PaymentsRelationReservations");

            migrationBuilder.DropColumn(
                name: "ReservationVehicleId",
                table: "PaymentsRelationReservations");
        }
    }
}
