using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class flight_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StatusFlight",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "StatusFlight",
                columns: new[] { "StatusFlightId", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "Activo" },
                    { 2, null, false, "Cancelado" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusFlight",
                keyColumn: "StatusFlightId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StatusFlight",
                keyColumn: "StatusFlightId",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StatusFlight",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
