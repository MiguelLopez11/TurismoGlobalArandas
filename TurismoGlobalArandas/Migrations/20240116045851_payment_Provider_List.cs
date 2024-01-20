using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class payment_Provider_List : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "PaymentProviders");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "PaymentProviders",
                newName: "ReservationDate");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "PaymentProviders",
                newName: "PaymentProviderId");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountMissing",
                table: "PaymentProviders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountTotal",
                table: "PaymentProviders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentProviderLists",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    PaymentConceptId = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviderLists", x => x.PaymentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentProviderLists");

            migrationBuilder.DropColumn(
                name: "AmountMissing",
                table: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "AmountTotal",
                table: "PaymentProviders");

            migrationBuilder.RenameColumn(
                name: "ReservationDate",
                table: "PaymentProviders",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "PaymentProviderId",
                table: "PaymentProviders",
                newName: "PaymentId");

            migrationBuilder.AddColumn<string>(
                name: "Invoice",
                table: "PaymentProviders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
