using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservations_habitations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Hotels_HotelId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "reservationInvoice",
                table: "Reservations",
                newName: "ReservationInvoice");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSale",
                table: "Reservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupCoordinator",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HabitationsReservationId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InlcudesBreakfast",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InlcudesTrasport",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberHabitations",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentLimitDate",
                table: "Reservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentPeriod",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneContact",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneCoordinator",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Promoter",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeHabitation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeReservationId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Habitations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Minors",
                table: "Habitations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Invoice",
                table: "Habitations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ages",
                table: "Habitations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Adults",
                table: "Habitations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "AdvancePayment",
                table: "Habitations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HabitationsReservations",
                columns: table => new
                {
                    HabitationsReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevertationId = table.Column<int>(type: "int", nullable: false),
                    HabitationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitationsReservations", x => x.HabitationsReservationId);
                });

            migrationBuilder.CreateTable(
                name: "SingleRate",
                columns: table => new
                {
                    SingleRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraDiscount = table.Column<double>(type: "float", nullable: false),
                    RateDiscounted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateClient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleRate", x => x.SingleRateId);
                });

            migrationBuilder.CreateTable(
                name: "TypeReservations",
                columns: table => new
                {
                    TypeReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeReservations", x => x.TypeReservationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TypeReservationId",
                table: "Reservations",
                column: "TypeReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Employees_EmployeeId",
                table: "Reservations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_HabitationsReservations_TypeReservationId",
                table: "Reservations",
                column: "TypeReservationId",
                principalTable: "HabitationsReservations",
                principalColumn: "HabitationsReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Hotels_HotelId",
                table: "Reservations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_TypeReservations_TypeReservationId",
                table: "Reservations",
                column: "TypeReservationId",
                principalTable: "TypeReservations",
                principalColumn: "TypeReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Employees_EmployeeId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_HabitationsReservations_TypeReservationId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Hotels_HotelId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_TypeReservations_TypeReservationId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "HabitationsReservations");

            migrationBuilder.DropTable(
                name: "SingleRate");

            migrationBuilder.DropTable(
                name: "TypeReservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TypeReservationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DateSale",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "GroupCoordinator",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "HabitationsReservationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "InlcudesBreakfast",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "InlcudesTrasport",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "NumberHabitations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PaymentLimitDate",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PaymentPeriod",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PhoneContact",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PhoneCoordinator",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Promoter",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TypeHabitation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TypeReservationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AdvancePayment",
                table: "Habitations");

            migrationBuilder.RenameColumn(
                name: "ReservationInvoice",
                table: "Reservations",
                newName: "reservationInvoice");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Habitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Minors",
                table: "Habitations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Invoice",
                table: "Habitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ages",
                table: "Habitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Adults",
                table: "Habitations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Hotels_HotelId",
                table: "Reservations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
