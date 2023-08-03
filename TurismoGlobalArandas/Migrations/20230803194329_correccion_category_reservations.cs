using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class correccion_category_reservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryReservations_Reservations_ReservationId",
                table: "CategoryReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_HabitationsReservations_TypeReservationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryReservations_ReservationId",
                table: "CategoryReservations");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "CategoryReservations");

            migrationBuilder.AddColumn<int>(
                name: "CategoryReservationId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CategoryReservationId",
                table: "Reservations",
                column: "CategoryReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HabitationsReservationId",
                table: "Reservations",
                column: "HabitationsReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_CategoryReservations_CategoryReservationId",
                table: "Reservations",
                column: "CategoryReservationId",
                principalTable: "CategoryReservations",
                principalColumn: "CategoryReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_HabitationsReservations_HabitationsReservationId",
                table: "Reservations",
                column: "HabitationsReservationId",
                principalTable: "HabitationsReservations",
                principalColumn: "HabitationsReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CategoryReservations_CategoryReservationId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_HabitationsReservations_HabitationsReservationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CategoryReservationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_HabitationsReservationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CategoryReservationId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "CategoryReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryReservations_ReservationId",
                table: "CategoryReservations",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryReservations_Reservations_ReservationId",
                table: "CategoryReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_HabitationsReservations_TypeReservationId",
                table: "Reservations",
                column: "TypeReservationId",
                principalTable: "HabitationsReservations",
                principalColumn: "HabitationsReservationId");
        }
    }
}
