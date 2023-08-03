using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class HabitationsReservation_habitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevertationId",
                table: "HabitationsReservations");

            migrationBuilder.AddColumn<string>(
                name: "ReservationInvoice",
                table: "HabitationsReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HabitationsReservations_HabitationId",
                table: "HabitationsReservations",
                column: "HabitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitationsReservations_Habitations_HabitationId",
                table: "HabitationsReservations",
                column: "HabitationId",
                principalTable: "Habitations",
                principalColumn: "HabitationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitationsReservations_Habitations_HabitationId",
                table: "HabitationsReservations");

            migrationBuilder.DropIndex(
                name: "IX_HabitationsReservations_HabitationId",
                table: "HabitationsReservations");

            migrationBuilder.DropColumn(
                name: "ReservationInvoice",
                table: "HabitationsReservations");

            migrationBuilder.AddColumn<int>(
                name: "RevertationId",
                table: "HabitationsReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
