using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chaos.Migrations
{
    /// <inheritdoc />
    public partial class organizationappsrelationsusermodelmodifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrgID",
                table: "USER",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ORGANIZATION",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "OrgID",
                table: "CHANNEL",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_OrgID",
                table: "USER",
                column: "OrgID");

            migrationBuilder.CreateIndex(
                name: "IX_CHANNEL_OrgID",
                table: "CHANNEL",
                column: "OrgID");

            migrationBuilder.AddForeignKey(
                name: "FK_CHANNEL_ORGANIZATION_OrgID",
                table: "CHANNEL",
                column: "OrgID",
                principalTable: "ORGANIZATION",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ORGANIZATION_OrgID",
                table: "USER",
                column: "OrgID",
                principalTable: "ORGANIZATION",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHANNEL_ORGANIZATION_OrgID",
                table: "CHANNEL");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ORGANIZATION_OrgID",
                table: "USER");

            migrationBuilder.DropIndex(
                name: "IX_USER_OrgID",
                table: "USER");

            migrationBuilder.DropIndex(
                name: "IX_CHANNEL_OrgID",
                table: "CHANNEL");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "OrgID",
                table: "CHANNEL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ORGANIZATION",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");
        }
    }
}
