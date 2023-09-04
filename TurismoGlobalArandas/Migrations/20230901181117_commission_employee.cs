using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class commission_employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAgency",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "DiscountClient",
                table: "Commissions");

            migrationBuilder.RenameColumn(
                name: "DiscountProviderId",
                table: "Commissions",
                newName: "CommissionId");

            migrationBuilder.AddColumn<double>(
                name: "CommissionAgency",
                table: "Commissions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CommissionClient",
                table: "Commissions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CommissionEmployee",
                table: "Commissions",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 1,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.10000000000000001, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 2,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.0, 0.14999999999999999 });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 3,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.14999999999999999, 0.10000000000000001, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 4,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.10000000000000001, 0.0, 0.10000000000000001 });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 5,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.17999999999999999, 0.13, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 6,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.17999999999999999, 0.10000000000000001, 0.080000000000000002 });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 7,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.10000000000000001, 0.0, 0.10000000000000001 });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 8,
                columns: new[] { "CommissionAgency", "CommissionClient", "CommissionEmployee" },
                values: new object[] { 0.050000000000000003, 0.0, 0.050000000000000003 });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "CommissionId", "Color", "CommissionAgency", "CommissionClient", "CommissionEmployee", "Description", "IsDeleted", "ProviderId" },
                values: new object[,]
                {
                    { 9, null, 0.14999999999999999, 0.080000000000000002, 0.070000000000000007, null, false, 9 },
                    { 10, null, 0.17000000000000001, 0.12, 0.050000000000000003, null, false, 10 },
                    { 11, null, 0.10000000000000001, 0.050000000000000003, 0.050000000000000003, null, false, 11 },
                    { 12, null, 0.10000000000000001, 0.0, 0.10000000000000001, null, false, 12 },
                    { 13, null, 0.20000000000000001, 0.0, 0.20000000000000001, null, false, 13 },
                    { 14, null, 0.050000000000000003, 0.0, 0.050000000000000003, null, false, 14 },
                    { 15, null, 0.14999999999999999, 0.10000000000000001, 0.050000000000000003, null, false, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "CommissionAgency",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "CommissionClient",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "CommissionEmployee",
                table: "Commissions");

            migrationBuilder.RenameColumn(
                name: "CommissionId",
                table: "Commissions",
                newName: "DiscountProviderId");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAgency",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountClient",
                table: "Commissions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 1,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.15m, 0.1m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 2,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.15m, 0m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 3,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.15m, 0.1m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 4,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.1m, 0m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 5,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.18m, 0.13m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 6,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.18m, 0.1m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 7,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.1m, 0m });

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "DiscountProviderId",
                keyValue: 8,
                columns: new[] { "DiscountAgency", "DiscountClient" },
                values: new object[] { 0.05m, 0m });
        }
    }
}
