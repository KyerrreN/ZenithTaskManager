using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class AddBossIdToTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BossId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BossId",
                table: "Tasks",
                column: "BossId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Bosses_BossId",
                table: "Tasks",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Bosses_BossId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_BossId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "BossId",
                table: "Tasks");
        }
    }
}
