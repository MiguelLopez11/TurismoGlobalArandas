using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservation_providers_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "ReservationTours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "ReservationFlights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTours_ProviderId",
                table: "ReservationTours",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_ProviderId",
                table: "ReservationFlights",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlights_Providers_ProviderId",
                table: "ReservationFlights",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTours_Providers_ProviderId",
                table: "ReservationTours",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlights_Providers_ProviderId",
                table: "ReservationFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTours_Providers_ProviderId",
                table: "ReservationTours");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTours_ProviderId",
                table: "ReservationTours");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlights_ProviderId",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "ReservationTours");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "ReservationFlights");
        }
    }
}
