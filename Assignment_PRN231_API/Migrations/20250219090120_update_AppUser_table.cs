using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class update_AppUser_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "AspNetUsers");
        }
    }
}
