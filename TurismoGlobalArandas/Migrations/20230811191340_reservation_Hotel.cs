using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservation_Hotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "CategoryReservations");

            migrationBuilder.CreateTable(
                name: "ReservationHotels",
                columns: table => new
                {
                    ReservationHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationInvoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeHabitation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberHabitations = table.Column<int>(type: "int", nullable: true),
                    PhoneContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupCoordinator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCoordinator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Promoter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLimitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TypeReservationId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    HabitationsReservationId = table.Column<int>(type: "int", nullable: true),
                    InlcudesTrasport = table.Column<bool>(type: "bit", nullable: false),
                    InlcudesBreakfast = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationHotels");

            migrationBuilder.CreateTable(
                name: "CategoryReservations",
                columns: table => new
                {
                    CategoryReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryReservations", x => x.CategoryReservationId);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryReservationId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    HabitationsReservationId = table.Column<int>(type: "int", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    TypeReservationId = table.Column<int>(type: "int", nullable: true),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupCoordinator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InlcudesBreakfast = table.Column<bool>(type: "bit", nullable: false),
                    InlcudesTrasport = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NumberHabitations = table.Column<int>(type: "int", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLimitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCoordinator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Promoter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationInvoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeHabitation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_CategoryReservations_CategoryReservationId",
                        column: x => x.CategoryReservationId,
                        principalTable: "CategoryReservations",
                        principalColumn: "CategoryReservationId");
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Reservations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Reservations_HabitationsReservations_HabitationsReservationId",
                        column: x => x.HabitationsReservationId,
                        principalTable: "HabitationsReservations",
                        principalColumn: "HabitationsReservationId");
                    table.ForeignKey(
                        name: "FK_Reservations_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                    table.ForeignKey(
                        name: "FK_Reservations_TypeReservations_TypeReservationId",
                        column: x => x.TypeReservationId,
                        principalTable: "TypeReservations",
                        principalColumn: "TypeReservationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CategoryReservationId",
                table: "Reservations",
                column: "CategoryReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HabitationsReservationId",
                table: "Reservations",
                column: "HabitationsReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HotelId",
                table: "Reservations",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TypeReservationId",
                table: "Reservations",
                column: "TypeReservationId");
        }
    }
}
