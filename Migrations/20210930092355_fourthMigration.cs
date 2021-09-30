using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PmsAPI.Migrations
{
    public partial class fourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Resources",
                newName: "ResourceID");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    EntityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    GroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.EntityID);
                    table.ForeignKey(
                        name: "FK_Entities_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entities_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TokenString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenID);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceID = table.Column<int>(type: "int", nullable: false),
                    EntityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Entities_EntityID",
                        column: x => x.EntityID,
                        principalTable: "Entities",
                        principalColumn: "EntityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Resources_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "Resources",
                        principalColumn: "ResourceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_EntityID",
                table: "Accounts",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ResourceID",
                table: "Accounts",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_GroupID",
                table: "Entities",
                column: "GroupID",
                unique: true,
                filter: "[GroupID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_UserID",
                table: "Entities",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserID",
                table: "Tokens",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "ResourceID",
                table: "Resources",
                newName: "Id");
        }
    }
}
