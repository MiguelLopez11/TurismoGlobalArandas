using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class Sale_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habitations",
                columns: table => new
                {
                    HabitationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ages = table.Column<int>(type: "int", nullable: false),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Minors = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitations", x => x.HabitationId);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reservationInvoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Hotel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeReservation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberHabitations = table.Column<int>(type: "int", nullable: true),
                    PhoneContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeVoucher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromoterId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PromoterId",
                table: "Sales",
                column: "PromoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ReservationId",
                table: "Sales",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habitations");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
