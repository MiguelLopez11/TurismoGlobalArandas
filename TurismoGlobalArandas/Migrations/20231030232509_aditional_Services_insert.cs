using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class aditional_Services_insert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AditionalServices",
                columns: new[] { "AditionalServiceId", "Cost", "Description", "IsDeleted", "Name", "ReservationFlightId", "ReservationVehicleId" },
                values: new object[,]
                {
                    { 1, null, null, false, "Servicio de reservación de vuelo", null, null },
                    { 2, null, null, false, "Servicio de reservación de vehiculo", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AditionalServices",
                keyColumn: "AditionalServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AditionalServices",
                keyColumn: "AditionalServiceId",
                keyValue: 2);
        }
    }
}
