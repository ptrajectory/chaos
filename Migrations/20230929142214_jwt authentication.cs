using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chaos.Migrations
{
    /// <inheritdoc />
    public partial class jwtauthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CLIENT_SECRET",
                table: "APPS",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TEST_CLIENT_SECRET",
                table: "APPS",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CLIENT_SECRET",
                table: "APPS");

            migrationBuilder.DropColumn(
                name: "TEST_CLIENT_SECRET",
                table: "APPS");
        }
    }
}
