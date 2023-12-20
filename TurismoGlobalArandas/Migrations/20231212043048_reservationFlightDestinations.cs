using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationFlightDestinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationFlightDestinations",
                columns: table => new
                {
                    ReservationFlightDestinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteDepartureAirportId = table.Column<int>(type: "int", nullable: true),
                    RouteArrivalAirportId = table.Column<int>(type: "int", nullable: true),
                    AirlineId = table.Column<int>(type: "int", nullable: true),
                    DateTravel = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationFlightDestinations", x => x.ReservationFlightDestinationId);
                    table.ForeignKey(
                        name: "FK_ReservationFlightDestinations_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "AirlineId");
                    table.ForeignKey(
                        name: "FK_ReservationFlightDestinations_Routes_RouteArrivalAirportId",
                        column: x => x.RouteArrivalAirportId,
                        principalTable: "Routes",
                        principalColumn: "RouteId");
                    table.ForeignKey(
                        name: "FK_ReservationFlightDestinations_Routes_RouteDepartureAirportId",
                        column: x => x.RouteDepartureAirportId,
                        principalTable: "Routes",
                        principalColumn: "RouteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlightDestinations_AirlineId",
                table: "ReservationFlightDestinations",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlightDestinations_RouteArrivalAirportId",
                table: "ReservationFlightDestinations",
                column: "RouteArrivalAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlightDestinations_RouteDepartureAirportId",
                table: "ReservationFlightDestinations",
                column: "RouteDepartureAirportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationFlightDestinations");
        }
    }
}
