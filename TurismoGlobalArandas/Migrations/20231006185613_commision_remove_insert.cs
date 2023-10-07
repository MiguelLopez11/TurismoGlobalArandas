using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TurismoGlobalArandas.Migrations
{
    /// <inheritdoc />
    public partial class commision_remove_insert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_Providers_ProviderId",
                table: "Commissions");

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "CommissionId",
                keyValue: 15);

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

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 15);

            migrationBuilder.RenameColumn(
                name: "GroupRateName",
                table: "ReservationHotelGroups",
                newName: "GroupName");

            migrationBuilder.RenameColumn(
                name: "Confirmation",
                table: "ReservationHotelGroups",
                newName: "ConfirmationKey");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Commissions",
                newName: "ServiceProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Commissions_ProviderId",
                table: "Commissions",
                newName: "IX_Commissions_ServiceProviderId");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeTotal",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangePublicClient",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeNight",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeMinor",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeJunior",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NightsNumber",
                table: "GroupRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MinorsWithoutCharge",
                table: "GroupRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MinorsCharge",
                table: "GroupRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Juniors",
                table: "GroupRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "GroupRates",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "GroupRates",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "AgesMinors",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Adults",
                table: "GroupRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "NamesCompanions",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionName",
                table: "Commissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ServicesProvider",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesProvider", x => x.ServiceId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_ServicesProvider_ServiceProviderId",
                table: "Commissions",
                column: "ServiceProviderId",
                principalTable: "ServicesProvider",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_ServicesProvider_ServiceProviderId",
                table: "Commissions");

            migrationBuilder.DropTable(
                name: "ServicesProvider");

            migrationBuilder.DropColumn(
                name: "NamesCompanions",
                table: "GroupRates");

            migrationBuilder.DropColumn(
                name: "CommissionName",
                table: "Commissions");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "ReservationHotelGroups",
                newName: "GroupRateName");

            migrationBuilder.RenameColumn(
                name: "ConfirmationKey",
                table: "ReservationHotelGroups",
                newName: "Confirmation");

            migrationBuilder.RenameColumn(
                name: "ServiceProviderId",
                table: "Commissions",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Commissions_ServiceProviderId",
                table: "Commissions",
                newName: "IX_Commissions_ProviderId");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeTotal",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RangePublicClient",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeNight",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeMinor",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeJunior",
                table: "GroupRates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NightsNumber",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinorsWithoutCharge",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinorsCharge",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Juniors",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "GroupRates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "GroupRates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgesMinors",
                table: "GroupRates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Adults",
                table: "GroupRates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "Email", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, false, "Record Hoteleria", null },
                    { 2, null, false, "Record Traslados", null },
                    { 3, null, false, "Check In Hoteleria", null },
                    { 4, null, false, "Check In renta de autos", null },
                    { 5, null, false, "Imacop", null },
                    { 6, null, false, "Imacop circuitos", null },
                    { 7, null, false, "Imacop traslados", null },
                    { 8, null, false, "Imacop entradas a disney", null },
                    { 9, null, false, "Promoser parques", null },
                    { 10, null, false, "Promoser hoteleria", null },
                    { 11, null, false, "Megatravel", null },
                    { 12, null, false, "Europamundo", null },
                    { 13, null, false, "Chartes", null },
                    { 14, null, false, "Israel traslados", null },
                    { 15, null, false, "Copper canyon", null }
                });

            migrationBuilder.InsertData(
                table: "TypeReservationGrupals",
                columns: new[] { "TypeReservationGrupalId", "Description", "IsDeleted", "Name" },
                values: new object[] { 2, null, false, "Individual" });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "CommissionId", "Color", "CommissionAgency", "CommissionClient", "CommissionEmployee", "Description", "IsDeleted", "ProviderId" },
                values: new object[,]
                {
                    { 1, null, "0.15", "0.10", "0.05", null, false, 1 },
                    { 2, null, "0.15", "0.00", "0.15", null, false, 2 },
                    { 3, null, "0.15", "0.10", "0.05", null, false, 3 },
                    { 4, null, "0.10", "0.00", "0.10", null, false, 4 },
                    { 5, null, "0.18", "0.13", "0.05", null, false, 5 },
                    { 6, null, "0.18", "0.10", "0.08", null, false, 6 },
                    { 7, null, "0.10", "0.00", "0.10", null, false, 7 },
                    { 8, null, "0.05", "0.00", "0.05", null, false, 8 },
                    { 9, null, "0.15", "0.08", "0.07", null, false, 9 },
                    { 10, null, "0.17", "0.12", "0.05", null, false, 10 },
                    { 11, null, "0.05", "0.05", "0.05", null, false, 11 },
                    { 12, null, "0.10", "0.00", "0.10", null, false, 12 },
                    { 13, null, "0.20", "0.00", "0.20", null, false, 13 },
                    { 14, null, "0.05", "0.00", "0.05", null, false, 14 },
                    { 15, null, "0.15", "0.10", "0.05", null, false, 15 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_Providers_ProviderId",
                table: "Commissions",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
