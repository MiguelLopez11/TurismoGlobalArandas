using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class service_provider_commision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 1,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 2,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 3,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 4,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 5,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 6,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 7,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 8,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 9,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 10,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 11,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 12,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 13,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 14,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0m, 0m, 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 15m, 5m });

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
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 10m, 5m });

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
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 5m, 5m });

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
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 10m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 12,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 20m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 13,
                columns: new[] { "CommissionAgency", "CommissionEmployee" },
                values: new object[] { 5m, 5m });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 14,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 15m, 10m, 5m });
        }
    }
}
