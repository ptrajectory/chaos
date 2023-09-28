using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chaos.Migrations
{
    /// <inheritdoc />
    public partial class organizationsapps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppID",
                table: "CHANNEL",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ORGANIZATION",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Banner = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGANIZATION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "APPS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    OrgID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_APPS_ORGANIZATION_OrgID",
                        column: x => x.OrgID,
                        principalTable: "ORGANIZATION",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHANNEL_AppID",
                table: "CHANNEL",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_APPS_OrgID",
                table: "APPS",
                column: "OrgID");

            migrationBuilder.AddForeignKey(
                name: "FK_CHANNEL_APPS_AppID",
                table: "CHANNEL",
                column: "AppID",
                principalTable: "APPS",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHANNEL_APPS_AppID",
                table: "CHANNEL");

            migrationBuilder.DropTable(
                name: "APPS");

            migrationBuilder.DropTable(
                name: "ORGANIZATION");

            migrationBuilder.DropIndex(
                name: "IX_CHANNEL_AppID",
                table: "CHANNEL");

            migrationBuilder.DropColumn(
                name: "AppID",
                table: "CHANNEL");
        }
    }
}
