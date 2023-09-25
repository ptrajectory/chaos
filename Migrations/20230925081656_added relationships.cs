using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chaos.Migrations
{
    /// <inheritdoc />
    public partial class addedrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PARTICIPANT_ChannelID",
                table: "PARTICIPANT",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_PARTICIPANT_UserID",
                table: "PARTICIPANT",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_ChannelID",
                table: "MESSAGE",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_SenderID",
                table: "MESSAGE",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_CHANNEL_CreatorID",
                table: "CHANNEL",
                column: "CreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_CHANNEL_USER_CreatorID",
                table: "CHANNEL",
                column: "CreatorID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MESSAGE_CHANNEL_ChannelID",
                table: "MESSAGE",
                column: "ChannelID",
                principalTable: "CHANNEL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MESSAGE_USER_SenderID",
                table: "MESSAGE",
                column: "SenderID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PARTICIPANT_CHANNEL_ChannelID",
                table: "PARTICIPANT",
                column: "ChannelID",
                principalTable: "CHANNEL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PARTICIPANT_USER_UserID",
                table: "PARTICIPANT",
                column: "UserID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHANNEL_USER_CreatorID",
                table: "CHANNEL");

            migrationBuilder.DropForeignKey(
                name: "FK_MESSAGE_CHANNEL_ChannelID",
                table: "MESSAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_MESSAGE_USER_SenderID",
                table: "MESSAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_PARTICIPANT_CHANNEL_ChannelID",
                table: "PARTICIPANT");

            migrationBuilder.DropForeignKey(
                name: "FK_PARTICIPANT_USER_UserID",
                table: "PARTICIPANT");

            migrationBuilder.DropIndex(
                name: "IX_PARTICIPANT_ChannelID",
                table: "PARTICIPANT");

            migrationBuilder.DropIndex(
                name: "IX_PARTICIPANT_UserID",
                table: "PARTICIPANT");

            migrationBuilder.DropIndex(
                name: "IX_MESSAGE_ChannelID",
                table: "MESSAGE");

            migrationBuilder.DropIndex(
                name: "IX_MESSAGE_SenderID",
                table: "MESSAGE");

            migrationBuilder.DropIndex(
                name: "IX_CHANNEL_CreatorID",
                table: "CHANNEL");
        }
    }
}
