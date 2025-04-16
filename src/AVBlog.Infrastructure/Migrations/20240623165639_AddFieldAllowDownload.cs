using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldAllowDownload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowDownload",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowDownload",
                table: "AspNetUsers");
        }
    }
}
