using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class payment_providers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentProviders",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: false),
                    ReservationTourId = table.Column<int>(type: "int", nullable: false),
                    ReservationFlightId = table.Column<int>(type: "int", nullable: false),
                    ReservationVehicleId = table.Column<int>(type: "int", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviders", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentProviders_ReservationFlights_ReservationFlightId",
                        column: x => x.ReservationFlightId,
                        principalTable: "ReservationFlights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentProviders_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentProviders_ReservationTours_ReservationTourId",
                        column: x => x.ReservationTourId,
                        principalTable: "ReservationTours",
                        principalColumn: "ReservationTourId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentProviders_ReservationVehicles_ReservationVehicleId",
                        column: x => x.ReservationVehicleId,
                        principalTable: "ReservationVehicles",
                        principalColumn: "ReservationVehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_ReservationFlightId",
                table: "PaymentProviders",
                column: "ReservationFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_ReservationHotelId",
                table: "PaymentProviders",
                column: "ReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_ReservationTourId",
                table: "PaymentProviders",
                column: "ReservationTourId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_ReservationVehicleId",
                table: "PaymentProviders",
                column: "ReservationVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentProviders");
        }
    }
}
