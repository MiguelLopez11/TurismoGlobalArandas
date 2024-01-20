using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class paymentProviderList_relation_concept_methods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviderLists_PaymentConceptId",
                table: "PaymentProviderLists",
                column: "PaymentConceptId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviderLists_PaymentMethodId",
                table: "PaymentProviderLists",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviderLists_PaymentConcepts_PaymentConceptId",
                table: "PaymentProviderLists",
                column: "PaymentConceptId",
                principalTable: "PaymentConcepts",
                principalColumn: "PaymentConceptId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviderLists_PaymentMethods_PaymentMethodId",
                table: "PaymentProviderLists",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviderLists_PaymentConcepts_PaymentConceptId",
                table: "PaymentProviderLists");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviderLists_PaymentMethods_PaymentMethodId",
                table: "PaymentProviderLists");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProviderLists_PaymentConceptId",
                table: "PaymentProviderLists");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProviderLists_PaymentMethodId",
                table: "PaymentProviderLists");
        }
    }
}
