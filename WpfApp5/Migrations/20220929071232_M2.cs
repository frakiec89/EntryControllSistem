using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp5.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Acaunts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acaunts_DepartmentId",
                table: "Acaunts",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acaunts_Department_DepartmentId",
                table: "Acaunts",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acaunts_Department_DepartmentId",
                table: "Acaunts");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Acaunts_DepartmentId",
                table: "Acaunts");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Acaunts");
        }
    }
}
