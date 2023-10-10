using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chaos.Migrations
{
    /// <inheritdoc />
    public partial class replying_to : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReplyingTo",
                table: "MESSAGE",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_ReplyingTo",
                table: "MESSAGE",
                column: "ReplyingTo");

            migrationBuilder.AddForeignKey(
                name: "FK_MESSAGE_MESSAGE_ReplyingTo",
                table: "MESSAGE",
                column: "ReplyingTo",
                principalTable: "MESSAGE",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MESSAGE_MESSAGE_ReplyingTo",
                table: "MESSAGE");

            migrationBuilder.DropIndex(
                name: "IX_MESSAGE_ReplyingTo",
                table: "MESSAGE");

            migrationBuilder.DropColumn(
                name: "ReplyingTo",
                table: "MESSAGE");
        }
    }
}
