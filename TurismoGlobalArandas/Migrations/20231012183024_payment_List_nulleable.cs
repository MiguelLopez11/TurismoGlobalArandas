using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class payment_List_nulleable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservationHotels_PaymentReservationHotelId",
                table: "PaymentRelationLists");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentReservationHotelId",
                table: "PaymentRelationLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "PaymentRelationLists",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "PaymentRelationLists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Invoice",
                table: "PaymentRelationLists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PaymentRelationLists",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservationHotels_PaymentReservationHotelId",
                table: "PaymentRelationLists",
                column: "PaymentReservationHotelId",
                principalTable: "PaymentsRelationReservationHotels",
                principalColumn: "PaymentReservationHotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservationHotels_PaymentReservationHotelId",
                table: "PaymentRelationLists");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentReservationHotelId",
                table: "PaymentRelationLists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "PaymentRelationLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "PaymentRelationLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Invoice",
                table: "PaymentRelationLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PaymentRelationLists",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRelationLists_PaymentsRelationReservationHotels_PaymentReservationHotelId",
                table: "PaymentRelationLists",
                column: "PaymentReservationHotelId",
                principalTable: "PaymentsRelationReservationHotels",
                principalColumn: "PaymentReservationHotelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
