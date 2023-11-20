using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class employee_reservations_created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ReservationVehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ReservationTours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ReservationFlights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationVehicles_EmployeeId",
                table: "ReservationVehicles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTours_EmployeeId",
                table: "ReservationTours",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_EmployeeId",
                table: "ReservationFlights",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlights_Employees_EmployeeId",
                table: "ReservationFlights",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTours_Employees_EmployeeId",
                table: "ReservationTours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationVehicles_Employees_EmployeeId",
                table: "ReservationVehicles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlights_Employees_EmployeeId",
                table: "ReservationFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTours_Employees_EmployeeId",
                table: "ReservationTours");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationVehicles_Employees_EmployeeId",
                table: "ReservationVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ReservationVehicles_EmployeeId",
                table: "ReservationVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTours_EmployeeId",
                table: "ReservationTours");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlights_EmployeeId",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ReservationVehicles");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ReservationTours");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ReservationFlights");
        }
    }
}
