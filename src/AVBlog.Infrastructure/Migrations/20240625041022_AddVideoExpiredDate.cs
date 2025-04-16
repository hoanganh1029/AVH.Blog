using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoExpiredDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "UserVimeoVideos",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "UserVimeoVideos");
        }
    }
}
