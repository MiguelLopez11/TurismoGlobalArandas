using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class detination_reservationHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_DestinationId",
                table: "ReservationHotels",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_Destinations_DestinationId",
                table: "ReservationHotels",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_Destinations_DestinationId",
                table: "ReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_DestinationId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "ReservationHotels");
        }
    }
}
