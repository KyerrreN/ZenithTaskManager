using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployerIdColumnToEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BossId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "BossId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BossId",
                table: "Employees",
                column: "BossId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Bosses_BossId",
                table: "Employees",
                column: "BossId",
                principalTable: "Bosses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Bosses_BossId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BossId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BossId",
                table: "Employees");
        }
    }
}
