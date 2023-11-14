using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class DateTravel_reservationFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelDateEnd",
                table: "ReservationFlights");

            migrationBuilder.RenameColumn(
                name: "TravelDateStart",
                table: "ReservationFlights",
                newName: "DateTravel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTravel",
                table: "ReservationFlights",
                newName: "TravelDateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "TravelDateEnd",
                table: "ReservationFlights",
                type: "datetime2",
                nullable: true);
        }
    }
}
