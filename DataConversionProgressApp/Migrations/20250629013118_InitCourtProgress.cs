using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConversionProgressApp.Migrations
{
    /// <inheritdoc />
    public partial class InitCourtProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourtProgressRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourtType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Court1Disposed = table.Column<bool>(type: "bit", nullable: false),
                    Court1DisposedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Court1Warrant = table.Column<bool>(type: "bit", nullable: false),
                    Court1WarrantBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtProgressRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourtProgressRecords");
        }
    }
}
