using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class invoice_reservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservationInvoice",
                table: "ReservationHotels",
                newName: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "Invoice",
                table: "ReservationTours",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "ReservationTours");

            migrationBuilder.RenameColumn(
                name: "Invoice",
                table: "ReservationHotels",
                newName: "ReservationInvoice");
        }
    }
}
