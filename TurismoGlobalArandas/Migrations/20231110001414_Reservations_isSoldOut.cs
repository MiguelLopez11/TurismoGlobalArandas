using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Reservations_isSoldOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSoldOut",
                table: "ReservationVehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoldOut",
                table: "ReservationTours",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoldOut",
                table: "ReservationFlights",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSoldOut",
                table: "ReservationVehicles");

            migrationBuilder.DropColumn(
                name: "IsSoldOut",
                table: "ReservationTours");

            migrationBuilder.DropColumn(
                name: "IsSoldOut",
                table: "ReservationFlights");
        }
    }
}
