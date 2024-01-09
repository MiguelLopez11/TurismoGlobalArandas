using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class removestatusinnecesary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsRelationReservations_StatusPaymentRelations_StatusPaymentRelationId",
                table: "PaymentsRelationReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFlights_StatusFlight_StatusFlightId",
                table: "ReservationFlights");

            migrationBuilder.DropTable(
                name: "StatusFlight");

            migrationBuilder.DropTable(
                name: "StatusPaymentRelations");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFlights_StatusFlightId",
                table: "ReservationFlights");

            migrationBuilder.DropIndex(
                name: "IX_PaymentsRelationReservations_StatusPaymentRelationId",
                table: "PaymentsRelationReservations");

            migrationBuilder.DropColumn(
                name: "StatusFlightId",
                table: "ReservationFlights");

            migrationBuilder.DropColumn(
                name: "StatusPaymentRelationId",
                table: "PaymentsRelationReservations");

            migrationBuilder.AddColumn<string>(
                name: "DetailsPayment",
                table: "PaymentRelationLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "PaymentRelationLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRelationLists_PaymentMethodId",
                table: "PaymentRelationLists",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRelationLists_PaymentMethods_PaymentMethodId",
                table: "PaymentRelationLists",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRelationLists_PaymentMethods_PaymentMethodId",
                table: "PaymentRelationLists");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRelationLists_PaymentMethodId",
                table: "PaymentRelationLists");

            migrationBuilder.DropColumn(
                name: "DetailsPayment",
                table: "PaymentRelationLists");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "PaymentRelationLists");

            migrationBuilder.AddColumn<int>(
                name: "StatusFlightId",
                table: "ReservationFlights",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusPaymentRelationId",
                table: "PaymentsRelationReservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatusFlight",
                columns: table => new
                {
                    StatusFlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusFlight", x => x.StatusFlightId);
                });

            migrationBuilder.CreateTable(
                name: "StatusPaymentRelations",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPaymentRelations", x => x.StatusId);
                });

            migrationBuilder.InsertData(
                table: "StatusFlight",
                columns: new[] { "StatusFlightId", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "Activo" },
                    { 2, null, false, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "StatusPaymentRelations",
                columns: new[] { "StatusId", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "Pendiente" },
                    { 2, null, false, "Liquidado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFlights_StatusFlightId",
                table: "ReservationFlights",
                column: "StatusFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsRelationReservations_StatusPaymentRelationId",
                table: "PaymentsRelationReservations",
                column: "StatusPaymentRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsRelationReservations_StatusPaymentRelations_StatusPaymentRelationId",
                table: "PaymentsRelationReservations",
                column: "StatusPaymentRelationId",
                principalTable: "StatusPaymentRelations",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFlights_StatusFlight_StatusFlightId",
                table: "ReservationFlights",
                column: "StatusFlightId",
                principalTable: "StatusFlight",
                principalColumn: "StatusFlightId");
        }
    }
}
