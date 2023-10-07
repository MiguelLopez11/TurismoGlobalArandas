using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class commisiontypedecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CommissionEmployee",
                table: "ServicesProviders",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CommissionClient",
                table: "ServicesProviders",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CommissionAgency",
                table: "ServicesProviders",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CommissionEmployee",
                table: "ServicesProviders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommissionClient",
                table: "ServicesProviders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommissionAgency",
                table: "ServicesProviders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 1,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.10", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 2,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 3,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.10", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 4,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.10", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 5,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.18", "0.13", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 6,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.18", "0.10", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 7,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.05", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 8,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.08", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 9,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.17", "0.12", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 10,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.10", "0.05", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 11,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.10", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 12,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.20", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 13,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.05", "0.00", "0.05" });

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 14,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { "0.15", "0.10", "0.05" });
        }
    }
}
