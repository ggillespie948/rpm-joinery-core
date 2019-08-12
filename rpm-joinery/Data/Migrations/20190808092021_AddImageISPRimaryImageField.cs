using Microsoft.EntityFrameworkCore.Migrations;

namespace rpm_joinery.Data.Migrations
{
    public partial class AddImageISPRimaryImageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryImage",
                table: "ProjectImages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimaryImage",
                table: "ProjectImages");
        }
    }
}
