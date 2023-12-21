using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Payment_date_limit_reservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentLimitDate",
                table: "ReservationVehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentLimitDateProvider",
                table: "ReservationVehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentLimitDate",
                table: "ReservationTours",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentLimitDateProvider",
                table: "ReservationTours",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentLimitDate",
                table: "ReservationFlights",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentLimitDateProvider",
                table: "ReservationFlights",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentLimitDate",
                table: "ReservationVehicles");

            migrationBuilder.DropColumn(
                name: "PaymentLimitDateProvider",
                table: "ReservationVehicles");

            migrationBuilder.DropColumn(
                name: "PaymentLimitDate",
                table: "ReservationTours");

            migrationBuilder.DropColumn(
                name: "PaymentLimitDateProvider",
                table: "ReservationTours");

            migrationBuilder.DropColumn(
                name: "PaymentLimitDate",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "PaymentLimitDateProvider",
                table: "ReservationFlights");
        }
    }
}
