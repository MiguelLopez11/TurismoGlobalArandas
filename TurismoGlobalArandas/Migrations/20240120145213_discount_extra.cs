using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class discount_extra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountExtra",
                table: "ServicesProviders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 1,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 2,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 3,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 4,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 5,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 6,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 7,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 8,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 9,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 10,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 11,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 12,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 13,
                column: "DiscountExtra",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ServicesProviders",
                keyColumn: "ServiceId",
                keyValue: 14,
                column: "DiscountExtra",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountExtra",
                table: "ServicesProviders");
        }
    }
}
