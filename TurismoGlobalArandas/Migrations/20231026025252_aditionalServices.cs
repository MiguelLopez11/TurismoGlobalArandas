using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class aditionalServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationFlightId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationVehicleId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AditionalServices",
                columns: table => new
                {
                    AditionalServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AditionalServices", x => x.AditionalServiceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_ReservationFlightId",
                table: "ReservationHotels",
                column: "ReservationFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_ReservationVehicleId",
                table: "ReservationHotels",
                column: "ReservationVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_ReservationFlights_ReservationFlightId",
                table: "ReservationHotels",
                column: "ReservationFlightId",
                principalTable: "ReservationFlights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_ReservationVehicles_ReservationVehicleId",
                table: "ReservationHotels",
                column: "ReservationVehicleId",
                principalTable: "ReservationVehicles",
                principalColumn: "ReservationVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_ReservationFlights_ReservationFlightId",
                table: "ReservationHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_ReservationVehicles_ReservationVehicleId",
                table: "ReservationHotels");

            migrationBuilder.DropTable(
                name: "AditionalServices");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_ReservationFlightId",
                table: "ReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_ReservationVehicleId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "ReservationFlightId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "ReservationVehicleId",
                table: "ReservationHotels");
        }
    }
}
