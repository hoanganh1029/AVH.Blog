using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "VimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "VimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedDate",
                table: "VimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VideoType",
                table: "VimeoVideos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserVimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "UserVimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "UserVimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedDate",
                table: "UserVimeoVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VimeoVideos");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VimeoVideos");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "VimeoVideos");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "VimeoVideos");

            migrationBuilder.DropColumn(
                name: "VideoType",
                table: "VimeoVideos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserVimeoVideos");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserVimeoVideos");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserVimeoVideos");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "UserVimeoVideos");
        }
    }
}
