using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class remove_commision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ServicesProvider",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ServicesProvider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionAgency",
                table: "ServicesProvider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionClient",
                table: "ServicesProvider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionEmployee",
                table: "ServicesProvider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "ServicesProvider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "Email", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, false, "Record", null },
                    { 2, null, false, "Check In", null },
                    { 3, null, false, "Imacop", null },
                    { 4, null, false, "Promoser", null },
                    { 5, null, false, "Megatravel", null },
                    { 6, null, false, "Europamundo", null },
                    { 7, null, false, "Chartes", null },
                    { 8, null, false, "Israel", null },
                    { 9, null, false, "Copper Canyon", null }
                });

            migrationBuilder.InsertData(
                table: "TypeReservationGrupals",
                columns: new[] { "TypeReservationGrupalId", "Description", "IsDeleted", "Name" },
                values: new object[] { 2, null, false, "Individual" });

            migrationBuilder.InsertData(
                table: "ServicesProvider",
                columns: new[] { "ServiceId", "Color", "CommissionAgency", "CommissionClient", "CommissionEmployee", "Description", "IsDeleted", "Name", "ProviderId" },
                values: new object[,]
                {
                    { 1, null, "0.15", "0.10", "0.05", null, false, "Hotelería", 1 },
                    { 2, null, "0.15", "0.00", "0.05", null, false, "Traslados", 1 },
                    { 3, null, "0.15", "0.10", "0.05", null, false, "Hoteleria", 2 },
                    { 4, null, "0.10", "0.00", "0.05", null, false, "Renta de autos", 2 },
                    { 5, null, "0.18", "0.13", "0.05", null, false, "Hoteleria", 3 },
                    { 6, null, "0.18", "0.10", "0.05", null, false, "Circuitos", 3 },
                    { 7, null, "0.05", "0.00", "0.05", null, false, "Entradas a disney", 3 },
                    { 8, null, "0.15", "0.08", "0.05", null, false, "Parques", 4 },
                    { 9, null, "0.17", "0.12", "0.05", null, false, "Hoteleria", 4 },
                    { 10, null, "0.10", "0.05", "0.05", null, false, "", 5 },
                    { 11, null, "0.10", "0.00", "0.05", null, false, "", 6 },
                    { 12, null, "0.20", "0.00", "0.05", null, false, "", 7 },
                    { 13, null, "0.05", "0.00", "0.05", null, false, "Traslados", 8 },
                    { 14, null, "0.15", "0.10", "0.05", null, false, "", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicesProvider_ProviderId",
                table: "ServicesProvider",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesProvider_Providers_ProviderId",
                table: "ServicesProvider",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesProvider_Providers_ProviderId",
                table: "ServicesProvider");

            migrationBuilder.DropIndex(
                name: "IX_ServicesProvider_ProviderId",
                table: "ServicesProvider");

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ServicesProvider",
                keyColumn: "ServiceId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TypeReservationGrupals",
                keyColumn: "TypeReservationGrupalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ServicesProvider");

            migrationBuilder.DropColumn(
                name: "CommissionAgency",
                table: "ServicesProvider");

            migrationBuilder.DropColumn(
                name: "CommissionClient",
                table: "ServicesProvider");

            migrationBuilder.DropColumn(
                name: "CommissionEmployee",
                table: "ServicesProvider");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "ServicesProvider");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ServicesProvider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    CommissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceProviderId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionAgency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.CommissionId);
                    table.ForeignKey(
                        name: "FK_Commissions_ServicesProvider_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServicesProvider",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_ServiceProviderId",
                table: "Commissions",
                column: "ServiceProviderId");
        }
    }
}
