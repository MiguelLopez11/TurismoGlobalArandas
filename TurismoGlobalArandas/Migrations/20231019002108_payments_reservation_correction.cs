using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class payments_reservation_correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentReservationId",
                table: "PaymentRelationLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentsRelationReservations",
                columns: table => new
                {
                    PaymentReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AmountMissing = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: true),
                    ReservationTourId = table.Column<int>(type: "int", nullable: true),
                    StatusPaymentRelationId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsRelationReservations", x => x.PaymentReservationId);
                    table.ForeignKey(
                        name: "FK_PaymentsRelationReservations_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId");
                    table.ForeignKey(
                        name: "FK_PaymentsRelationReservations_ReservationTours_ReservationTourId",
                        column: x => x.ReservationTourId,
                        principalTable: "ReservationTours",
                        principalColumn: "ReservationTourId");
                    table.ForeignKey(
                        name: "FK_PaymentsRelationReservations_StatusPaymentRelations_StatusPaymentRelationId",
                        column: x => x.StatusPaymentRelationId,
                        principalTable: "StatusPaymentRelations",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRelationLists_PaymentReservationId",
                table: "PaymentRelationLists",
                column: "PaymentReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservations_ReservationHotelId",
                table: "PaymentsRelationReservations",
                column: "ReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservations_ReservationTourId",
                table: "PaymentsRelationReservations",
                column: "ReservationTourId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservations_StatusPaymentRelationId",
                table: "PaymentsRelationReservations",
                column: "StatusPaymentRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservations_PaymentReservationId",
                table: "PaymentRelationLists",
                column: "PaymentReservationId",
                principalTable: "PaymentsRelationReservations",
                principalColumn: "PaymentReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservations_PaymentReservationId",
                table: "PaymentRelationLists");

            migrationBuilder.DropTable(
                name: "PaymentsRelationReservations");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRelationLists_PaymentReservationId",
                table: "PaymentRelationLists");

            migrationBuilder.DropColumn(
                name: "PaymentReservationId",
                table: "PaymentRelationLists");
        }
    }
}
