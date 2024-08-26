using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToBossAndEmployeeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bosses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bosses");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BossId", "Department", "Employer", "Name", "Patronymic", "Position", "Surname" },
                values: new object[] { 1, null, "OASU", "Kreerenko", "Vadim", "Pavlovich", "Operator EVM", "Podlipny" });
        }
    }
}
