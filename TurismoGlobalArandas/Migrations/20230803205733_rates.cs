using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class rates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleRate");

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RateTypeId = table.Column<int>(type: "int", nullable: false),
                    IndividualHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoubleHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TripleHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QDPLHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JuniorHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinorHabitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgesMinors = table.Column<int>(type: "int", nullable: false),
                    AgesJuniors = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.RateId);
                });

            migrationBuilder.CreateTable(
                name: "RateTypes",
                columns: table => new
                {
                    RateTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTypes", x => x.RateTypeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "RateTypes");

            migrationBuilder.CreateTable(
                name: "SingleRate",
                columns: table => new
                {
                    SingleRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraDiscount = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PublicRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateClient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateDiscounted = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleRate", x => x.SingleRateId);
                });
        }
    }
}
