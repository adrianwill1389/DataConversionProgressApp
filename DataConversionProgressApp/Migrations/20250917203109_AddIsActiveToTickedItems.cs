using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConversionProgressApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToTickedItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeenAnnouncementTime",
                table: "UserAccounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TickedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickedItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TickedItems");

            migrationBuilder.DropColumn(
                name: "LastSeenAnnouncementTime",
                table: "UserAccounts");
        }
    }
}
