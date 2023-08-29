using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class rates_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "RateTypes");

            migrationBuilder.CreateTable(
                name: "GroupRates",
                columns: table => new
                {
                    GroupRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: false),
                    IndividualHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DoubleHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TripleHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QDPLHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    JuniorHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinorHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgesMinors = table.Column<int>(type: "int", nullable: true),
                    AgesJuniors = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRates", x => x.GroupRateId);
                    table.ForeignKey(
                        name: "FK_GroupRates_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualRates",
                columns: table => new
                {
                    IndividualRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationHotelId = table.Column<int>(type: "int", nullable: false),
                    PublicRate = table.Column<double>(type: "float", nullable: false),
                    ClientRate = table.Column<double>(type: "float", nullable: false),
                    ExtraDiscount = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualRates", x => x.IndividualRateId);
                    table.ForeignKey(
                        name: "FK_IndividualRates_ReservationHotels_ReservationHotelId",
                        column: x => x.ReservationHotelId,
                        principalTable: "ReservationHotels",
                        principalColumn: "ReservationHotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupRates_ReservationHotelId",
                table: "GroupRates",
                column: "ReservationHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualRates_ReservationHotelId",
                table: "IndividualRates",
                column: "ReservationHotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupRates");

            migrationBuilder.DropTable(
                name: "IndividualRates");

            migrationBuilder.CreateTable(
                name: "RateTypes",
                columns: table => new
                {
                    RateTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTypes", x => x.RateTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RateTypeId = table.Column<int>(type: "int", nullable: false),
                    AgesJuniors = table.Column<int>(type: "int", nullable: false),
                    AgesMinors = table.Column<int>(type: "int", nullable: false),
                    DoubleHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndividualHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    JuniorHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinorHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QDPLHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TripleHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.RateId);
                    table.ForeignKey(
                        name: "FK_Rates_RateTypes_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateTypes",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_RateTypeId",
                table: "Rates",
                column: "RateTypeId");
        }
    }
}
