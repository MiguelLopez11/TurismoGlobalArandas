using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class rate_group_reservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupRates_ReservationHotels_ReservationHotelId",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "Confirmation",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "DateArrival",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "GroupRateName",
                table: "GroupRates");

            migrationBuilder.RenameColumn(
                name: "ReservationHotelId",
                table: "GroupRates",
                newName: "ReservationHotelGroupId");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "GroupRates",
                newName: "Observations");

            migrationBuilder.RenameColumn(
                name: "Coordinator",
                table: "GroupRates",
                newName: "AgesMinors");

            migrationBuilder.RenameIndex(
                name: "IX_GroupRates_ReservationHotelId",
                table: "GroupRates",
                newName: "IX_GroupRates_ReservationHotelGroupId");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "GroupRates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "GroupRates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Juniors",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinorsCharge",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinorsWithoutCharge",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NightsNumber",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeJunior",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeMinor",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeNight",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RangePublicClient",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RangeTotal",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ReservationHotelGroups",
                columns: table => new
                {
                    ReservationHotelGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupRateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confirmation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateArrival = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Coordinator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHotelGroups", x => x.ReservationHotelGroupId);
                    table.ForeignKey(
                        name: "FK_ReservationHotelGroups_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelGroups_ReservationHotelId",
                table: "ReservationHotelGroups",
                column: "ReservationHotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupRates_ReservationHotelGroups_ReservationHotelGroupId",
                table: "GroupRates",
                column: "ReservationHotelGroupId",
                principalTable: "ReservationHotelGroups",
                principalColumn: "ReservationHotelGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupRates_ReservationHotelGroups_ReservationHotelGroupId",
                table: "GroupRates");

            migrationBuilder.DropTable(
                name: "ReservationHotelGroups");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "Juniors",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "MinorsCharge",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "MinorsWithoutCharge",
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

            migrationBuilder.RenameColumn(
                name: "ReservationHotelGroupId",
                table: "GroupRates",
                newName: "ReservationHotelId");

            migrationBuilder.RenameColumn(
                name: "Observations",
                table: "GroupRates",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "AgesMinors",
                table: "GroupRates",
                newName: "Coordinator");

            migrationBuilder.RenameIndex(
                name: "IX_GroupRates_ReservationHotelGroupId",
                table: "GroupRates",
                newName: "IX_GroupRates_ReservationHotelId");

            migrationBuilder.AddColumn<string>(
                name: "Confirmation",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_GroupRates_ReservationHotels_ReservationHotelId",
                table: "GroupRates",
                column: "ReservationHotelId",
                principalTable: "ReservationHotels",
                principalColumn: "ReservationHotelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
