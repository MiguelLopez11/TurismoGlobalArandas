using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Reservation_Tours_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationTours",
                columns: table => new
                {
                    ReservationTourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationId = table.Column<int>(type: "int", nullable: true),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateActivity = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberAdults = table.Column<int>(type: "int", nullable: true),
                    NumberMinors = table.Column<int>(type: "int", nullable: true),
                    AdultCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinorCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PublicRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExchangeRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInternational = table.Column<bool>(type: "bit", nullable: false),
                    IsNational = table.Column<bool>(type: "bit", nullable: false),
                    IncludeTransportation = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTours", x => x.ReservationTourId);
                    table.ForeignKey(
                        name: "FK_ReservationTours_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTours_DestinationId",
                table: "ReservationTours",
                column: "DestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationTours");
        }
    }
}
