using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class TypeReservationGrupal_Range : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeReservationGrupalId",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupRates_TypeReservationGrupalId",
                table: "GroupRates",
                column: "TypeReservationGrupalId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupRates_TypeReservationGrupals_TypeReservationGrupalId",
                table: "GroupRates",
                column: "TypeReservationGrupalId",
                principalTable: "TypeReservationGrupals",
                principalColumn: "TypeReservationGrupalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupRates_TypeReservationGrupals_TypeReservationGrupalId",
                table: "GroupRates");

            migrationBuilder.DropIndex(
                name: "IX_GroupRates_TypeReservationGrupalId",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "TypeReservationGrupalId",
                table: "GroupRates");
        }
    }
}
