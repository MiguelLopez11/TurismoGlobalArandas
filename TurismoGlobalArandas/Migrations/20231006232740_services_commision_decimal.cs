using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class services_commision_decimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionEmployee",
                table: "ServicesProviders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionClient",
                table: "ServicesProviders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionAgency",
                table: "ServicesProviders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 1,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 15m, 10m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 2,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 15m, 0m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 3,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 15m, 10m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 4,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 10m, 0m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 5,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 18m, 13m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 6,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 18m, 10m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 7,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 5m, 0m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 8,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 15m, 8m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 9,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 17m, 12m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 10,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 10m, 5m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 11,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 10m, 0m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 12,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 20m, 0m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 13,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 5m, 0m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 14,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 15m, 10m, 5m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CommissionEmployee",
                table: "ServicesProviders",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "CommissionClient",
                table: "ServicesProviders",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "CommissionAgency",
                table: "ServicesProviders",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 1,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.10000000000000001, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 2,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.0, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 3,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.10000000000000001, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 4,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.10000000000000001, 0.0, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 5,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.17999999999999999, 0.13, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 6,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.17999999999999999, 0.10000000000000001, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 7,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.050000000000000003, 0.0, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 8,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.080000000000000002, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 9,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.17000000000000001, 0.12, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 10,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.10000000000000001, 0.050000000000000003, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 11,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.10000000000000001, 0.0, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 12,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.20000000000000001, 0.0, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 13,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.050000000000000003, 0.0, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 14,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.10000000000000001, 0.050000000000000003 });
        }
    }
}
