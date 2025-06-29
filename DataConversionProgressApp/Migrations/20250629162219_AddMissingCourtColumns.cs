using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConversionProgressApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingCourtColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Court1Night",
                table: "CourtProgressRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Court1NightBy",
                table: "CourtProgressRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Court2Disposed",
                table: "CourtProgressRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Court2DisposedBy",
                table: "CourtProgressRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Court2Night",
                table: "CourtProgressRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Court2NightBy",
                table: "CourtProgressRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Court2Warrant",
                table: "CourtProgressRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Court2WarrantBy",
                table: "CourtProgressRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Court3Night",
                table: "CourtProgressRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Court3NightBy",
                table: "CourtProgressRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Court1Night",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court1NightBy",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court2Disposed",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court2DisposedBy",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court2Night",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court2NightBy",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court2Warrant",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court2WarrantBy",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court3Night",
                table: "CourtProgressRecords");

            migrationBuilder.DropColumn(
                name: "Court3NightBy",
                table: "CourtProgressRecords");
        }
    }
}
