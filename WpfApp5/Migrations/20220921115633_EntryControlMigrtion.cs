using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp5.Migrations
{
    public partial class EntryControlMigrtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acaunts",
                columns: table => new
                {
                    AcauntId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acaunts", x => x.AcauntId);
                });

            migrationBuilder.CreateTable(
                name: "EntryControls",
                columns: table => new
                {
                    EntryControlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeEntryControl = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcauntId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryControls", x => x.EntryControlId);
                    table.ForeignKey(
                        name: "FK_EntryControls_Acaunts_AcauntId",
                        column: x => x.AcauntId,
                        principalTable: "Acaunts",
                        principalColumn: "AcauntId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryControls_AcauntId",
                table: "EntryControls",
                column: "AcauntId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryControls");

            migrationBuilder.DropTable(
                name: "Acaunts");
        }
    }
}
