using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Rate_Relation_RateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_RateTypeId",
                table: "Rates",
                column: "RateTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_RateTypes_RateTypeId",
                table: "Rates",
                column: "RateTypeId",
                principalTable: "RateTypes",
                principalColumn: "RateTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_RateTypes_RateTypeId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_RateTypeId",
                table: "Rates");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoterId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    CodeVoucher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NumberHabitations = table.Column<int>(type: "int", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeReservation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Employees_PromoterId",
                        column: x => x.PromoterId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PromoterId",
                table: "Sales",
                column: "PromoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ReservationId",
                table: "Sales",
                column: "ReservationId");
        }
    }
}
