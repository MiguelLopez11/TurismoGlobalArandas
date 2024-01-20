using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Employees_relation_payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "PaymentRelationLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRelationLists_EmployeeId",
                table: "PaymentRelationLists",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRelationLists_Employees_EmployeeId",
                table: "PaymentRelationLists",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRelationLists_Employees_EmployeeId",
                table: "PaymentRelationLists");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRelationLists_EmployeeId",
                table: "PaymentRelationLists");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PaymentRelationLists");
        }
    }
}
