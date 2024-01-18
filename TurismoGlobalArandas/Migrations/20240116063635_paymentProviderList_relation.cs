using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class paymentProviderList_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentProviderId",
                table: "PaymentProviderLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviderLists_PaymentProviderId",
                table: "PaymentProviderLists",
                column: "PaymentProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviderLists_PaymentProviders_PaymentProviderId",
                table: "PaymentProviderLists",
                column: "PaymentProviderId",
                principalTable: "PaymentProviders",
                principalColumn: "PaymentProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviderLists_PaymentProviders_PaymentProviderId",
                table: "PaymentProviderLists");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProviderLists_PaymentProviderId",
                table: "PaymentProviderLists");

            migrationBuilder.DropColumn(
                name: "PaymentProviderId",
                table: "PaymentProviderLists");
        }
    }
}
