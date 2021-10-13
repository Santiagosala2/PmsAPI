using Microsoft.EntityFrameworkCore.Migrations;

namespace PmsAPI.Migrations
{
    public partial class sixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaltHashedPassword",
                table: "Users",
                newName: "SaltedHashedPassword");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaltedHashedPassword",
                table: "Users",
                newName: "SaltHashedPassword");
        }
    }
}
