using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class type_reservation_insert_context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeReservations",
                keyColumn: "TypeReservationId",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TypeReservations",
                columns: new[] { "TypeReservationId", "Description", "IsDeleted", "Name" },
                values: new object[] { 3, null, false, "Grupo" });
        }
    }
}
