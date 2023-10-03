using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservation_grupal_columns_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupRateName",
                table: "ReservationHotelGroups",
                newName: "GroupName");

            migrationBuilder.RenameColumn(
                name: "Confirmation",
                table: "ReservationHotelGroups",
                newName: "ConfirmationKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "ReservationHotelGroups",
                newName: "GroupRateName");

            migrationBuilder.RenameColumn(
                name: "ConfirmationKey",
                table: "ReservationHotelGroups",
                newName: "Confirmation");
        }
    }
}
