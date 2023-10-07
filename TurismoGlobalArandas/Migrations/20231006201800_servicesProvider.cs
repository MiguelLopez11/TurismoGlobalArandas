using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class servicesProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesProvider_Providers_ProviderId",
                table: "ServicesProvider");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicesProvider",
                table: "ServicesProvider");

            migrationBuilder.RenameTable(
                name: "ServicesProvider",
                newName: "ServicesProviders");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesProvider_ProviderId",
                table: "ServicesProviders",
                newName: "IX_ServicesProviders_ProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicesProviders",
                table: "ServicesProviders",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesProviders_Providers_ProviderId",
                table: "ServicesProviders",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesProviders_Providers_ProviderId",
                table: "ServicesProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicesProviders",
                table: "ServicesProviders");

            migrationBuilder.RenameTable(
                name: "ServicesProviders",
                newName: "ServicesProvider");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesProviders_ProviderId",
                table: "ServicesProvider",
                newName: "IX_ServicesProvider_ProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicesProvider",
                table: "ServicesProvider",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesProvider_Providers_ProviderId",
                table: "ServicesProvider",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
