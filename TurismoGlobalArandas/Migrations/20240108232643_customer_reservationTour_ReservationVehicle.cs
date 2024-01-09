using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class customer_reservationTour_ReservationVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ReservationVehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ReservationTours",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationVehicles_CustomerId",
                table: "ReservationVehicles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTours_CustomerId",
                table: "ReservationTours",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTours_Customers_CustomerId",
                table: "ReservationTours",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationVehicles_Customers_CustomerId",
                table: "ReservationVehicles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTours_Customers_CustomerId",
                table: "ReservationTours");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationVehicles_Customers_CustomerId",
                table: "ReservationVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ReservationVehicles_CustomerId",
                table: "ReservationVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTours_CustomerId",
                table: "ReservationTours");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ReservationVehicles");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ReservationTours");
        }
    }
}
