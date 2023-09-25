using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chaos.Migrations
{
    /// <inheritdoc />
    public partial class uploadfileurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MEDIA_UPLOADS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FileUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDIA_UPLOADS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE_MEDIA",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    MessageID = table.Column<string>(type: "text", nullable: true),
                    MediaID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE_MEDIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MESSAGE_MEDIA_MEDIA_UPLOADS_MediaID",
                        column: x => x.MediaID,
                        principalTable: "MEDIA_UPLOADS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MESSAGE_MEDIA_MESSAGE_MessageID",
                        column: x => x.MessageID,
                        principalTable: "MESSAGE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_MEDIA_MediaID",
                table: "MESSAGE_MEDIA",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_MEDIA_MessageID",
                table: "MESSAGE_MEDIA",
                column: "MessageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MESSAGE_MEDIA");

            migrationBuilder.DropTable(
                name: "MEDIA_UPLOADS");
        }
    }
}
