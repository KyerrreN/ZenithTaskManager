using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMToMRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Bosses_BossId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "DepartmentAndBosses");

            migrationBuilder.DropIndex(
                name: "IX_Departments_BossId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "BossId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Bosses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bosses_DepartmentId",
                table: "Bosses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bosses_Departments_DepartmentId",
                table: "Bosses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bosses_Departments_DepartmentId",
                table: "Bosses");

            migrationBuilder.DropIndex(
                name: "IX_Bosses_DepartmentId",
                table: "Bosses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Bosses");

            migrationBuilder.AddColumn<int>(
                name: "BossId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Departments_BossId",
                table: "Departments",
                column: "BossId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentAndBosses_BossId",
                table: "DepartmentAndBosses",
                column: "BossId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Bosses_BossId",
                table: "Departments",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
