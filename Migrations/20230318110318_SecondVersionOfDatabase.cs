using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumTask.Migrations
{
    /// <inheritdoc />
    public partial class SecondVersionOfDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentImages",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentImages",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}