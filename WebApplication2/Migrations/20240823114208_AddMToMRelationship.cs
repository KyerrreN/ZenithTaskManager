using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class AddMToMRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentAndBosses",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    BossId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentAndBosses", x => new { x.DepartmentId, x.BossId });
                    table.ForeignKey(
                        name: "FK_DepartmentAndBosses_Bosses_BossId",
                        column: x => x.BossId,
                        principalTable: "Bosses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentAndBosses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentAndBosses_BossId",
                table: "DepartmentAndBosses",
                column: "BossId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentAndBosses");
        }
    }
}
