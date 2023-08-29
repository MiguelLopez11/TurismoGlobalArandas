using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class provider_discounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiscountProviders",
                columns: table => new
                {
                    DiscountProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountProviders", x => x.DiscountProviderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_ProviderId",
                table: "ReservationHotels",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_Providers_ProviderId",
                table: "ReservationHotels",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_Providers_ProviderId",
                table: "ReservationHotels");

            migrationBuilder.DropTable(
                name: "DiscountProviders");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_ProviderId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "ReservationHotels");
        }
    }
}
