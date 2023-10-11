using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class PaymentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusPaymentRelations",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPaymentRelations", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentsReservationHotels",
                columns: table => new
                {
                    PaymentReservationHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AmountMissing = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: true),
                    StatusPaymentRelationId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsReservationHotels", x => x.PaymentReservationHotelId);
                    table.ForeignKey(
                        name: "FK_PaymentsReservationHotels_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId");
                    table.ForeignKey(
                        name: "FK_PaymentsReservationHotels_StatusPaymentRelations_StatusPaymentRelationId",
                        column: x => x.StatusPaymentRelationId,
                        principalTable: "StatusPaymentRelations",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentRelationLists",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReservationHotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRelationLists", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentRelationLists_PaymentsReservationHotels_PaymentReservationHotelId",
                        column: x => x.PaymentReservationHotelId,
                        principalTable: "PaymentsReservationHotels",
                        principalColumn: "PaymentReservationHotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRelationLists_PaymentReservationHotelId",
                table: "PaymentRelationLists",
                column: "PaymentReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsReservationHotels_ReservationHotelId",
                table: "PaymentsReservationHotels",
                column: "ReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsReservationHotels_StatusPaymentRelationId",
                table: "PaymentsReservationHotels",
                column: "StatusPaymentRelationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRelationLists");

            migrationBuilder.DropTable(
                name: "PaymentsReservationHotels");

            migrationBuilder.DropTable(
                name: "StatusPaymentRelations");
        }
    }
}
