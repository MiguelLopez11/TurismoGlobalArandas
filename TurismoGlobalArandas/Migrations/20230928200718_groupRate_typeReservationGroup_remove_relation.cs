using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class groupRate_typeReservationGroup_remove_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TypeReservationGroupId",
                table: "ReservationHotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_TypeReservationGroupId",
                table: "ReservationHotels",
                column: "TypeReservationGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotels_TypeReservationGrupals_TypeReservationGroupId",
                table: "ReservationHotels",
                column: "TypeReservationGroupId",
                principalTable: "TypeReservationGrupals",
                principalColumn: "TypeReservationGrupalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotels_TypeReservationGrupals_TypeReservationGroupId",
                table: "ReservationHotels");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHotels_TypeReservationGroupId",
                table: "ReservationHotels");

            migrationBuilder.DropColumn(
                name: "TypeReservationGroupId",
                table: "ReservationHotels");

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
    }
}
