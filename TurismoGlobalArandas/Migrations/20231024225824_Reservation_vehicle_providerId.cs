using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Reservation_vehicle_providerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationVehicles_Providers_ProveedorId",
                table: "ReservationVehicles");

            migrationBuilder.RenameColumn(
                name: "ProveedorId",
                table: "ReservationVehicles",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationVehicles_ProveedorId",
                table: "ReservationVehicles",
                newName: "IX_ReservationVehicles_ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationVehicles_Providers_ProviderId",
                table: "ReservationVehicles",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationVehicles_Providers_ProviderId",
                table: "ReservationVehicles");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "ReservationVehicles",
                newName: "ProveedorId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationVehicles_ProviderId",
                table: "ReservationVehicles",
                newName: "IX_ReservationVehicles_ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationVehicles_Providers_ProveedorId",
                table: "ReservationVehicles",
                column: "ProveedorId",
                principalTable: "Providers",
                principalColumn: "ProviderId");
        }
    }
}
