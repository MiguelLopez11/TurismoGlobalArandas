using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservation_hotelsId_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_HabitationsReservations_HabitationsReservationId",
                table: "ReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_HabitationsReservationId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "HabitationsReservationId",
                table: "ReservationHotels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HabitationsReservationId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_HabitationsReservationId",
                table: "ReservationHotels",
                column: "HabitationsReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_HabitationsReservations_HabitationsReservationId",
                table: "ReservationHotels",
                column: "HabitationsReservationId",
                principalTable: "HabitationsReservations",
                principalColumn: "HabitationsReservationId");
        }
    }
}
