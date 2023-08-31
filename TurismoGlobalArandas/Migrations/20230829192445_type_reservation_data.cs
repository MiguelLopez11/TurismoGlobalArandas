using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class type_reservation_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TypeReservations",
                columns: new[] { "TypeReservationId", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "Individual" },
                    { 2, null, false, "Grupal" },
                    { 3, null, false, "Grupo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeReservations",
                keyColumn: "TypeReservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypeReservations",
                keyColumn: "TypeReservationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypeReservations",
                keyColumn: "TypeReservationId",
                keyValue: 3);
        }
    }
}
