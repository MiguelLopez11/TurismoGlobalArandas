using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class repair_reservationHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitationsReservations_ReservationHotels_ReservationHotelId",
                table: "HabitationsReservations");

            migrationBuilder.DropTable(
                name: "ReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_HabitationsReservations_ReservationHotelId",
                table: "HabitationsReservations");

            migrationBuilder.DropColumn(
                name: "ReservationHotelId",
                table: "HabitationsReservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationHotelId",
                table: "HabitationsReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReservationHotels",
                columns: table => new
                {
                    ReservationHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    HabitationsReservationId = table.Column<int>(type: "int", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    TypeReservationId = table.Column<int>(type: "int", nullable: true),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupCoordinator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NumberHabitations = table.Column<int>(type: "int", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLimitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCoordinator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Promoter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TypeHabitation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHotels", x => x.ReservationHotelId);
                    table.ForeignKey(
                        name: "FK_ReservationHotels_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_ReservationHotels_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_ReservationHotels_HabitationsReservations_HabitationsReservationId",
                        column: x => x.HabitationsReservationId,
                        principalTable: "HabitationsReservations",
                        principalColumn: "HabitationsReservationId");
                    table.ForeignKey(
                        name: "FK_ReservationHotels_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                    table.ForeignKey(
                        name: "FK_ReservationHotels_TypeReservations_TypeReservationId",
                        column: x => x.TypeReservationId,
                        principalTable: "TypeReservations",
                        principalColumn: "TypeReservationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitationsReservations_ReservationHotelId",
                table: "HabitationsReservations",
                column: "ReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_CustomerId",
                table: "ReservationHotels",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_EmployeeId",
                table: "ReservationHotels",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_HabitationsReservationId",
                table: "ReservationHotels",
                column: "HabitationsReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_HotelId",
                table: "ReservationHotels",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_TypeReservationId",
                table: "ReservationHotels",
                column: "TypeReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitationsReservations_ReservationHotels_ReservationHotelId",
                table: "HabitationsReservations",
                column: "ReservationHotelId",
                principalTable: "ReservationHotels",
                principalColumn: "ReservationHotelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
