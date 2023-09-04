using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class changeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountProviders");

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    DiscountProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountAgency = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountClient = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.DiscountProviderId);
                    table.ForeignKey(
                        name: "FK_Commissions_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "DiscountProviderId", "Color", "Description", "DiscountAgency", "DiscountClient", "IsDeleted", "ProviderId" },
                values: new object[,]
                {
                    { 1, null, null, 0.15m, 0.1m, false, 1 },
                    { 2, null, null, 0.15m, 0m, false, 2 },
                    { 3, null, null, 0.15m, 0.1m, false, 3 },
                    { 4, null, null, 0.1m, 0m, false, 4 },
                    { 5, null, null, 0.18m, 0.13m, false, 5 },
                    { 6, null, null, 0.18m, 0.1m, false, 6 },
                    { 7, null, null, 0.1m, 0m, false, 7 },
                    { 8, null, null, 0.05m, 0m, false, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_ProviderId",
                table: "Commissions",
                column: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.CreateTable(
                name: "DiscountProviders",
                columns: table => new
                {
                    DiscountProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountAgency = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountClient = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountProviders", x => x.DiscountProviderId);
                    table.ForeignKey(
                        name: "FK_DiscountProviders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DiscountProviders",
                columns: new[] { "DiscountProviderId", "Color", "Description", "DiscountAgency", "DiscountClient", "IsDeleted", "ProviderId" },
                values: new object[] { 1, null, null, 0.15m, null, false, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProviders_ProviderId",
                table: "DiscountProviders",
                column: "ProviderId");
        }
    }
}
