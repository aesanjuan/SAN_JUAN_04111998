using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace short_clips_web_api.Migrations
{
    /// <inheritdoc />
    public partial class Add_VideoContentType_Column_In_dboVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoContentType",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoContentType",
                table: "Videos");
        }
    }
}
