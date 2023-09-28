using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chaos.Migrations
{
    /// <inheritdoc />
    public partial class addusersapplink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppID",
                table: "USER",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_AppID",
                table: "USER",
                column: "AppID");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_APPS_AppID",
                table: "USER",
                column: "AppID",
                principalTable: "APPS",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_APPS_AppID",
                table: "USER");

            migrationBuilder.DropIndex(
                name: "IX_USER_AppID",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "AppID",
                table: "USER");
        }
    }
}
