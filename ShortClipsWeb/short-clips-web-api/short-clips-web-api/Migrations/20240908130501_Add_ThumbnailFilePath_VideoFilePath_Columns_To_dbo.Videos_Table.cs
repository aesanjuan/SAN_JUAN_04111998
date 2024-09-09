using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace short_clips_web_api.Migrations
{
    /// <inheritdoc />
    public partial class Add_ThumbnailFilePath_VideoFilePath_Columns_To_dboVideos_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFilePath",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoFilePath",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailFilePath",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoFilePath",
                table: "Videos");
        }
    }
}
