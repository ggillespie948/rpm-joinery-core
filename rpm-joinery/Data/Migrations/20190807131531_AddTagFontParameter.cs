using Microsoft.EntityFrameworkCore.Migrations;

namespace rpm_joinery.Data.Migrations
{
    public partial class AddTagFontParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FontSize",
                table: "Tags",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FontSize",
                table: "Tags");
        }
    }
}
