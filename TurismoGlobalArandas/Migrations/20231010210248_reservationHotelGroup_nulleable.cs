using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationHotelGroup_nulleable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotelGroups_ReservationHotels_ReservationHotelId",
                table: "ReservationHotelGroups");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationHotelId",
                table: "ReservationHotelGroups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ReservationHotelGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Coordinator",
                table: "ReservationHotelGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotelGroups_ReservationHotels_ReservationHotelId",
                table: "ReservationHotelGroups",
                column: "ReservationHotelId",
                principalTable: "ReservationHotels",
                principalColumn: "ReservationHotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotelGroups_ReservationHotels_ReservationHotelId",
                table: "ReservationHotelGroups");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationHotelId",
                table: "ReservationHotelGroups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ReservationHotelGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Coordinator",
                table: "ReservationHotelGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotelGroups_ReservationHotels_ReservationHotelId",
                table: "ReservationHotelGroups",
                column: "ReservationHotelId",
                principalTable: "ReservationHotels",
                principalColumn: "ReservationHotelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
