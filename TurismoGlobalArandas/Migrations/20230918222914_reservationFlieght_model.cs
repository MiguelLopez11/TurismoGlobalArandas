using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationFlieght_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateBirth",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReservationFlights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelDateStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelDateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartureAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmationKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePublic = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    isSimpleFlight = table.Column<bool>(type: "bit", nullable: false),
                    isRoundFlight = table.Column<bool>(type: "bit", nullable: false),
                    isMultidestinationFlight = table.Column<bool>(type: "bit", nullable: false),
                    PaymentMethodAgency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationFlights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_ReservationFlights_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_CustomerId",
                table: "ReservationFlights",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "DateBirth",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Customers");
        }
    }
}
