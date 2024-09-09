using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace short_clips_web_api.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Thumbnail_VideoContent_Columns_From_dboVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoContent",
                table: "Videos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoContent",
                table: "Videos",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
