using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationHotelGroup_corrections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "NightsNumber",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "RangeJunior",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "RangeMinor",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "RangeNight",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "RangePublicClient",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "RangeTotal",
                table: "GroupRates");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "ReservationHotelGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "ReservationHotelGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NightsNumber",
                table: "ReservationHotelGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeJunior",
                table: "ReservationHotelGroups",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeMinor",
                table: "ReservationHotelGroups",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeNight",
                table: "ReservationHotelGroups",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangePublicClient",
                table: "ReservationHotelGroups",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeTotal",
                table: "ReservationHotelGroups",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "NightsNumber",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "RangeJunior",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "RangeMinor",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "RangeNight",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "RangePublicClient",
                table: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "RangeTotal",
                table: "ReservationHotelGroups");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "GroupRates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "GroupRates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NightsNumber",
                table: "GroupRates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeJunior",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeMinor",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeNight",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangePublicClient",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeTotal",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
