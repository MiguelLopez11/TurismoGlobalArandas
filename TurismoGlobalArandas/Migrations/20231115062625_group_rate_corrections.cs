using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class group_rate_corrections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RangeNight",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "RangeTotal",
                table: "ReservationHotelGroups");

            migrationBuilder.AddColumn<decimal>(
                name: "RangeNight",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeTotal",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RangeNight",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "RangeTotal",
                table: "GroupRates");

            migrationBuilder.AddColumn<decimal>(
                name: "RangeNight",
                table: "ReservationHotelGroups",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeTotal",
                table: "ReservationHotelGroups",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
