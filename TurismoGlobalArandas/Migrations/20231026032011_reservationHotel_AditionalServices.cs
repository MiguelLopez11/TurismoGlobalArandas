using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class reservationHotel_AditionalServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationHotelsServicesAditionals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: false),
                    AditionalServiceId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHotelsServicesAditionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationHotelsServicesAditionals_AditionalServices_AditionalServiceId",
                        column: x => x.AditionalServiceId,
                        principalTable: "AditionalServices",
                        principalColumn: "AditionalServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationHotelsServicesAditionals_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelsServicesAditionals_AditionalServiceId",
                table: "ReservationHotelsServicesAditionals",
                column: "AditionalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelsServicesAditionals_ReservationHotelId",
                table: "ReservationHotelsServicesAditionals",
                column: "ReservationHotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationHotelsServicesAditionals");
        }
    }
}
