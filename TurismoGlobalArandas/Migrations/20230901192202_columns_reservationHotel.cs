using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class columns_reservationHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationPolicy",
                table: "ReservationHotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeVoicher",
                table: "ReservationHotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "ReservationHotels",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationPolicy",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "CodeVoicher",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "ReservationHotels");
        }
    }
}
