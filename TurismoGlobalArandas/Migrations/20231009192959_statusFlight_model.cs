using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class statusFlight_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "StatusFlight");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StatusFlight",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StatusFlight");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "StatusFlight",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
