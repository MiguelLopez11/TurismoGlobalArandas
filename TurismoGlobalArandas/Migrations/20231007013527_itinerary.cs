using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class itinerary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Itineraries",
            //    columns: table => new
            //    {
            //        itineraryId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        User = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Itineraries", x => x.itineraryId);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itineraries");
        }
    }
}
