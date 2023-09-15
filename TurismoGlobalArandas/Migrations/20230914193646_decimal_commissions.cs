using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class decimal_commissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 15);

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

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 15);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionEmployee",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionClient",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionAgency",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CommissionEmployee",
                table: "Commissions",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CommissionClient",
                table: "Commissions",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CommissionAgency",
                table: "Commissions",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "Email", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, false, "Record Hoteleria", null },
                    { 2, null, false, "Record Traslados", null },
                    { 3, null, false, "Check In Hoteleria", null },
                    { 4, null, false, "Check In renta de autos", null },
                    { 5, null, false, "Imacop", null },
                    { 6, null, false, "Imacop circuitos", null },
                    { 7, null, false, "Imacop traslados", null },
                    { 8, null, false, "Imacop entradas a disney", null },
                    { 9, null, false, "Promoser parques", null },
                    { 10, null, false, "Promoser hoteleria", null },
                    { 11, null, false, "Megatravel", null },
                    { 12, null, false, "Europamundo", null },
                    { 13, null, false, "Chartes", null },
                    { 14, null, false, "Israel traslados", null },
                    { 15, null, false, "Copper canyon", null }
                });

            migrationBuilder.InsertData(
                table: "TypeReservations",
                columns: new[] { "TypeReservationId", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "Individual" },
                    { 2, null, false, "Grupal" },
                    { 3, null, false, "Grupo" }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "CommissionId", "Color", "CommissionAgency", "CommissionClient", "CommissionEmployee", "Description", "IsDeleted", "ProviderId" },
                values: new object[,]
                {
                    { 1, null, 0.14999999999999999, 0.10000000000000001, 0.050000000000000003, null, false, 1 },
                    { 2, null, 0.14999999999999999, 0.0, 0.14999999999999999, null, false, 2 },
                    { 3, null, 0.14999999999999999, 0.10000000000000001, 0.050000000000000003, null, false, 3 },
                    { 4, null, 0.10000000000000001, 0.0, 0.10000000000000001, null, false, 4 },
                    { 5, null, 0.17999999999999999, 0.13, 0.050000000000000003, null, false, 5 },
                    { 6, null, 0.17999999999999999, 0.10000000000000001, 0.080000000000000002, null, false, 6 },
                    { 7, null, 0.10000000000000001, 0.0, 0.10000000000000001, null, false, 7 },
                    { 8, null, 0.050000000000000003, 0.0, 0.050000000000000003, null, false, 8 },
                    { 9, null, 0.14999999999999999, 0.080000000000000002, 0.070000000000000007, null, false, 9 },
                    { 10, null, 0.17000000000000001, 0.12, 0.050000000000000003, null, false, 10 },
                    { 11, null, 0.10000000000000001, 0.050000000000000003, 0.050000000000000003, null, false, 11 },
                    { 12, null, 0.10000000000000001, 0.0, 0.10000000000000001, null, false, 12 },
                    { 13, null, 0.20000000000000001, 0.0, 0.20000000000000001, null, false, 13 },
                    { 14, null, 0.050000000000000003, 0.0, 0.050000000000000003, null, false, 14 },
                    { 15, null, 0.14999999999999999, 0.10000000000000001, 0.050000000000000003, null, false, 15 }
                });
        }
    }
}
