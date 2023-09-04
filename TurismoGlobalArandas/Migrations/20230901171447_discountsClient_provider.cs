using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class discountsClient_provider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "DiscountProviders",
                newName: "DiscountClient");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAgency",
                table: "DiscountProviders",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAgency",
                table: "DiscountProviders");

            migrationBuilder.RenameColumn(
                name: "DiscountClient",
                table: "DiscountProviders",
                newName: "Discount");
        }
    }
}
