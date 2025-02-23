using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class update_AppUser_table_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58afbb9d-dde4-41ae-87e2-41c0d5c38805");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7a35253-5630-455e-b350-5390b4c7622d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecfeba61-9946-44dc-9d7b-db1a3ccaabca");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d1b6804-56ba-437e-909d-e9330f9c9e1c", "99dcd667-8bbb-4b8f-8683-1229c9d9ec05", "Staff", "STAFF" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a467648c-1446-45d0-a935-3907ec248627", "0e31c0fd-d339-4570-91c8-4213dea7555f", "Owner", "OWNER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a610b6c1-0962-4bdf-88f6-37a5ef926fad", "cbf3745a-75c9-4cc0-817d-46f92f3ec480", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d1b6804-56ba-437e-909d-e9330f9c9e1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a467648c-1446-45d0-a935-3907ec248627");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a610b6c1-0962-4bdf-88f6-37a5ef926fad");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58afbb9d-dde4-41ae-87e2-41c0d5c38805", "45311180-9e56-4122-ab88-aba8e373ea45", "Owner", "OWNER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a7a35253-5630-455e-b350-5390b4c7622d", "d1f94036-c7ef-41a2-b7b0-e4239dba601b", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecfeba61-9946-44dc-9d7b-db1a3ccaabca", "84cfa5c0-e674-4f57-9e72-4565578ae6d2", "Staff", "STAFF" });
        }
    }
}
