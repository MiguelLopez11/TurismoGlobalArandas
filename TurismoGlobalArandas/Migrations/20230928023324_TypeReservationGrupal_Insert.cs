using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class TypeReservationGrupal_Insert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeReservationGrupals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TypeReservationGrupals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "TypeReservationGrupals",
                columns: new[] { "TypeReservationGrupalId", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "Grupo" },
                    { 2, null, false, "Individual" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeReservationGrupals",
                keyColumn: "TypeReservationGrupalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypeReservationGrupals",
                keyColumn: "TypeReservationGrupalId",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeReservationGrupals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TypeReservationGrupals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
