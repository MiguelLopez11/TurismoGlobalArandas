using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class type_reservation_grupal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgesJuniors",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "AgesMinors",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "DoubleHabitation",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "IndividualHabitation",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "JuniorHabitation",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "MinorHabitation",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "QDPLHabitation",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "TripleHabitation",
                table: "GroupRates");

            migrationBuilder.AddColumn<string>(
                name: "Confirmation",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinator",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateArrival",
                table: "GroupRates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupRateName",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TypeReservationGrupals",
                columns: table => new
                {
                    TypeReservationGrupalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeReservationGrupals", x => x.TypeReservationGrupalId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeReservationGrupals");

            migrationBuilder.DropColumn(
                name: "Confirmation",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "Coordinator",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "DateArrival",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "GroupRateName",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "GroupRates");

            migrationBuilder.AddColumn<int>(
                name: "AgesJuniors",
                table: "GroupRates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgesMinors",
                table: "GroupRates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DoubleHabitation",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IndividualHabitation",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "JuniorHabitation",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinorHabitation",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "QDPLHabitation",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TripleHabitation",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
