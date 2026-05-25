using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookhouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverUrlToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Books",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Books");
        }
    }
}
