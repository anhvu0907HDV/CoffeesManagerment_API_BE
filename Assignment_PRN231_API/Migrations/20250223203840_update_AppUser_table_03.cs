using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class update_AppUser_table_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e4f6728-b6ed-4b36-91f3-5e13e858f153");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cc01932-c19c-48e7-97f7-8c5006309c2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89a7b3d7-3e84-40ef-afe9-1311624d44ec");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e4f6728-b6ed-4b36-91f3-5e13e858f153", "0282809d-9e65-410c-aa37-57a880b1a6a1", "Staff", "STAFF" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5cc01932-c19c-48e7-97f7-8c5006309c2a", "19c12dd2-b878-4af8-a742-aaa3c2bea98c", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89a7b3d7-3e84-40ef-afe9-1311624d44ec", "cb919aa5-4eec-4f49-9b8f-12b378e59200", "Owner", "OWNER" });
        }
    }
}
