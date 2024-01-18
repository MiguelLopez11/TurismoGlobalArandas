using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class payment_Provider_list_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentConceptId",
                table: "PaymentProviders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "PaymentProviders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PaymentProviderLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_PaymentConceptId",
                table: "PaymentProviders",
                column: "PaymentConceptId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_PaymentMethodId",
                table: "PaymentProviders",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_PaymentConcepts_PaymentConceptId",
                table: "PaymentProviders",
                column: "PaymentConceptId",
                principalTable: "PaymentConcepts",
                principalColumn: "PaymentConceptId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_PaymentMethods_PaymentMethodId",
                table: "PaymentProviders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_PaymentConcepts_PaymentConceptId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_PaymentMethods_PaymentMethodId",
                table: "PaymentProviders");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProviders_PaymentConceptId",
                table: "PaymentProviders");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProviders_PaymentMethodId",
                table: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "PaymentConceptId",
                table: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PaymentProviderLists");
        }
    }
}
