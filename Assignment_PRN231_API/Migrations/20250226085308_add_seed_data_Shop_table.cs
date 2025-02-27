using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class add_seed_data_Shop_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f4af42a-9257-4599-8ce2-acbdd5d85932");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28947a14-ab64-420c-a388-442adc0066a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88b3fe29-d2e4-457c-bcfd-c3b4514d0c3b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "67a12207-d160-4653-ae2d-e9812f7d494f", "90c4e92e-a448-40fd-b319-e6f95b919bfb", "Staff", "STAFF" },
                    { "698c9691-9617-4358-af6a-a47cd359c2d9", "30d301c8-37d3-47a8-b420-ddca75cb9f0a", "Owner", "OWNER" },
                    { "8e3ed2c0-2017-4c81-ada8-643c1cca52ae", "f2d982f8-0c17-40b1-afb4-0fdc37a4172f", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St", "Shop A", "0123456789" },
                    { 2, "456 Elm St", "Shop B", "0987654321" },
                    { 3, "789 Oak St", "Shop C", "0345678901" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67a12207-d160-4653-ae2d-e9812f7d494f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "698c9691-9617-4358-af6a-a47cd359c2d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e3ed2c0-2017-4c81-ada8-643c1cca52ae");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0f4af42a-9257-4599-8ce2-acbdd5d85932", "fc002e1c-1c7c-4809-8987-f99ce3bfccd1", "Staff", "STAFF" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28947a14-ab64-420c-a388-442adc0066a3", "fd351d52-fef4-4a80-af25-69fc38a1d96f", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88b3fe29-d2e4-457c-bcfd-c3b4514d0c3b", "f81831be-e295-414e-ae14-03a2267e96c3", "Owner", "OWNER" });
        }
    }
}
