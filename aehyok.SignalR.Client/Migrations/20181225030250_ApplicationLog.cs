using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aehyok.SignalR.Client.Migrations
{
    public partial class ApplicationLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppMenuId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Application = table.Column<string>(maxLength: 50, nullable: false),
                    Logged = table.Column<DateTime>(nullable: false),
                    Level = table.Column<string>(maxLength: 50, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Logger = table.Column<string>(maxLength: 250, nullable: true),
                    Callsite = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMenu",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FatherId = table.Column<int>(nullable: false),
                    MetaParameter = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Controller = table.Column<string>(maxLength: 40, nullable: true),
                    Action = table.Column<string>(maxLength: 40, nullable: true),
                    AppRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMenu_AspNetRoles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_AppMenuId",
                table: "AspNetRoles",
                column: "AppMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMenu_AppRoleId",
                table: "AppMenu",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AppMenu_AppMenuId",
                table: "AspNetRoles",
                column: "AppMenuId",
                principalTable: "AppMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AppMenu_AppMenuId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ApplicationLog");

            migrationBuilder.DropTable(
                name: "AppMenu");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_AppMenuId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AppMenuId",
                table: "AspNetRoles");
        }
    }
}
