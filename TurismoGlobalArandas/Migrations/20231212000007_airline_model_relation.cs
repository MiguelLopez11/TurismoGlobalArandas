using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class airline_model_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Airline",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "ArrivalAirport",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "DepartureAirport",
                table: "ReservationFlights");

            migrationBuilder.AddColumn<int>(
                name: "AirlineId",
                table: "ReservationFlights",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteArrivalAirportId",
                table: "ReservationFlights",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteDepartureAirportId",
                table: "ReservationFlights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_AirlineId",
                table: "ReservationFlights",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_RouteArrivalAirportId",
                table: "ReservationFlights",
                column: "RouteArrivalAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_RouteDepartureAirportId",
                table: "ReservationFlights",
                column: "RouteDepartureAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlights_Airlines_AirlineId",
                table: "ReservationFlights",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "AirlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlights_Routes_RouteArrivalAirportId",
                table: "ReservationFlights",
                column: "RouteArrivalAirportId",
                principalTable: "Routes",
                principalColumn: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlights_Routes_RouteDepartureAirportId",
                table: "ReservationFlights",
                column: "RouteDepartureAirportId",
                principalTable: "Routes",
                principalColumn: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlights_Airlines_AirlineId",
                table: "ReservationFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlights_Routes_RouteArrivalAirportId",
                table: "ReservationFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlights_Routes_RouteDepartureAirportId",
                table: "ReservationFlights");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlights_AirlineId",
                table: "ReservationFlights");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlights_RouteArrivalAirportId",
                table: "ReservationFlights");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlights_RouteDepartureAirportId",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "AirlineId",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "RouteArrivalAirportId",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "RouteDepartureAirportId",
                table: "ReservationFlights");

            migrationBuilder.AddColumn<string>(
                name: "Airline",
                table: "ReservationFlights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArrivalAirport",
                table: "ReservationFlights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartureAirport",
                table: "ReservationFlights",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
