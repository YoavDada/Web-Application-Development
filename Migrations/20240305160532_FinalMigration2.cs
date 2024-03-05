using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coursework.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_ManagerEmployeeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerEmployeeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ManagerEmployeeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerEmployeeId",
                table: "Projects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerEmployeeId",
                table: "Projects",
                column: "ManagerEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_ManagerEmployeeId",
                table: "Projects",
                column: "ManagerEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
