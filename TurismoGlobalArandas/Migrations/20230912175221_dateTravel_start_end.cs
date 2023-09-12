using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class dateTravel_start_end : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TravelDate",
                table: "ReservationHotels",
                newName: "TravelDateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "TravelDateEnd",
                table: "ReservationHotels",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelDateEnd",
                table: "ReservationHotels");

            migrationBuilder.RenameColumn(
                name: "TravelDateStart",
                table: "ReservationHotels",
                newName: "TravelDate");
        }
    }
}
