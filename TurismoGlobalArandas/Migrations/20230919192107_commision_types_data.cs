using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class commision_types_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CommissionEmployee",
                table: "Commissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommissionClient",
                table: "Commissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommissionAgency",
                table: "Commissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 1,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.10", "0.05" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 2,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.00", "0.15" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 3,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.10", "0.05" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 4,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.10", "0.00", "0.10" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 5,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.18", "0.13", "0.05" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 6,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.18", "0.10", "0.08" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 7,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.10", "0.00", "0.10" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 8,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.05", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 9,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.08", "0.07" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 10,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.17", "0.12", "0.05" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 11,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.05", "0.05", "0.05" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 12,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.10", "0.00", "0.10" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 13,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.20", "0.00", "0.20" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 14,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.05", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 15,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.10", "0.05" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionEmployee",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionClient",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionAgency",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 1,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.15m, 0.1m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 2,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.15m, 0m, 0.15m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 3,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.15m, 0.1m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 4,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.1m, 0m, 0.1m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 5,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.18m, 0.13m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 6,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.18m, 0.1m, 0.08m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 7,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.1m, 0m, 0.1m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 8,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.05m, 0m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 9,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.15m, 0.08m, 0.07m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 10,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.17m, 0.12m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 11,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.05m, 0.05m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 12,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.1m, 0m, 0.1m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 13,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.2m, 0m, 0.2m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 14,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.05m, 0m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 15,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.15m, 0.1m, 0.05m });
        }
    }
}
