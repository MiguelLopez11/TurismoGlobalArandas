using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class removepaymentsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservationHotels_PaymentReservationHotelId",
                table: "PaymentRelationLists");

            migrationBuilder.DropTable(
                name: "PaymentsRelationReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRelationLists_PaymentReservationHotelId",
                table: "PaymentRelationLists");

            migrationBuilder.DropColumn(
                name: "PaymentReservationHotelId",
                table: "PaymentRelationLists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentReservationHotelId",
                table: "PaymentRelationLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentsRelationReservationHotels",
                columns: table => new
                {
                    PaymentReservationHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: true),
                    StatusPaymentRelationId = table.Column<int>(type: "int", nullable: true),
                    AmountMissing = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AmountTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsRelationReservationHotels", x => x.PaymentReservationHotelId);
                    table.ForeignKey(
                        name: "FK_PaymentsRelationReservationHotels_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId");
                    table.ForeignKey(
                        name: "FK_PaymentsRelationReservationHotels_StatusPaymentRelations_StatusPaymentRelationId",
                        column: x => x.StatusPaymentRelationId,
                        principalTable: "StatusPaymentRelations",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRelationLists_PaymentReservationHotelId",
                table: "PaymentRelationLists",
                column: "PaymentReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservationHotels_ReservationHotelId",
                table: "PaymentsRelationReservationHotels",
                column: "ReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservationHotels_StatusPaymentRelationId",
                table: "PaymentsRelationReservationHotels",
                column: "StatusPaymentRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservationHotels_PaymentReservationHotelId",
                table: "PaymentRelationLists",
                column: "PaymentReservationHotelId",
                principalTable: "PaymentsRelationReservationHotels",
                principalColumn: "PaymentReservationHotelId");
        }
    }
}
