using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservation_hotel_habitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationHotelId",
                table: "HabitationsReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HabitationsReservations_ReservationHotelId",
                table: "HabitationsReservations",
                column: "ReservationHotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitationsReservations_ReservationHotels_ReservationHotelId",
                table: "HabitationsReservations",
                column: "ReservationHotelId",
                principalTable: "ReservationHotels",
                principalColumn: "ReservationHotelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitationsReservations_ReservationHotels_ReservationHotelId",
                table: "HabitationsReservations");

            migrationBuilder.DropIndex(
                name: "IX_HabitationsReservations_ReservationHotelId",
                table: "HabitationsReservations");

            migrationBuilder.DropColumn(
                name: "ReservationHotelId",
                table: "HabitationsReservations");
        }
    }
}
