using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class relation_files_paymentProviders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentProviderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_Files_PaymentProviders_PaymentProviderId",
                        column: x => x.PaymentProviderId,
                        principalTable: "PaymentProviders",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_PaymentProviderId",
                table: "Files",
                column: "PaymentProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
