using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class add_seed_data_AppUser_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a6cbab21-3ac7-41d4-9248-ad788c440350", "3d5dc647-5d96-4de1-9d01-9ea90a3b03c7", "Staff", "STAFF" },
                    { "c55bedfc-9833-4973-8582-983cbbc0d4fb", "db752a25-03d7-427b-990b-7f0cb23f4a50", "Manager", "MANAGER" },
                    { "f6490579-2f9d-4b05-9398-8929cdf0bba9", "ba8aa66f-4600-4649-b2e8-4e03cbfcfc4d", "Owner", "OWNER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Avatar", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "18e765a4-71fe-44c7-9494-ef3736a0b231", 0, 28, null, new DateTime(1996, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "d324f1d4-034b-4403-bc53-7d5b9a34c66f", "staff1@example.com", true, "Bob", "Tran", false, null, "STAFF1@EXAMPLE.COM", "STAFF1", "AQAAAAEAACcQAAAAEIOw9rR5Ct5LUsBTa3G1ljknC6r6qnUIpZtLS99vXuh1oROK3exjlcEyvEtoKDG/8g==", null, false, "47560f11-0c23-415d-9831-2ba73e0a46f0", false, "staff1" },
                    { "6ad3d737-1557-4194-9d1a-e8cee0cd8e43", 0, 40, null, new DateTime(1984, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "7c361396-936e-4b80-8e03-2e2fbe66c3dd", "owner1@example.com", true, "Alice", "Nguyen", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAEAACcQAAAAEK6EMeANBHBJbA50eIA4VnY12Op9/AQk5mtL29qRBTBeKuzbgo2A/WIWcPeRXtgdYw==", null, false, "62a343bc-dd25-43c7-a06e-0e8b3d0ccfd5", false, "owner1" },
                    { "9f189f16-f2c9-4525-8e1e-e85d09acfb1b", 0, 35, null, new DateTime(1989, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "092cf17b-327b-4288-874c-25ce8a999bb5", "manager1@example.com", true, "Charlie", "Pham", false, null, "MANAGER1@EXAMPLE.COM", "MANAGER1", "AQAAAAEAACcQAAAAEMsYTVGDHIxr36zWB7unKkzWQaQO8sVuYBxupOj4vXfaibCSv6r5OUTuPggnx30NoA==", null, false, "e92d7eb2-b7d5-4ed0-bf92-cda6e30ffc09", false, "manager1" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a6cbab21-3ac7-41d4-9248-ad788c440350", "18e765a4-71fe-44c7-9494-ef3736a0b231" },
                    { "f6490579-2f9d-4b05-9398-8929cdf0bba9", "6ad3d737-1557-4194-9d1a-e8cee0cd8e43" },
                    { "c55bedfc-9833-4973-8582-983cbbc0d4fb", "9f189f16-f2c9-4525-8e1e-e85d09acfb1b" }
                });

            migrationBuilder.InsertData(
                table: "UserShops",
                columns: new[] { "Id", "Role", "ShopId", "UserId" },
                values: new object[,]
                {
                    { -2, "Manager", 2, "9f189f16-f2c9-4525-8e1e-e85d09acfb1b" },
                    { -1, "Manager", 1, "9f189f16-f2c9-4525-8e1e-e85d09acfb1b" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a6cbab21-3ac7-41d4-9248-ad788c440350", "18e765a4-71fe-44c7-9494-ef3736a0b231" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f6490579-2f9d-4b05-9398-8929cdf0bba9", "6ad3d737-1557-4194-9d1a-e8cee0cd8e43" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c55bedfc-9833-4973-8582-983cbbc0d4fb", "9f189f16-f2c9-4525-8e1e-e85d09acfb1b" });

            migrationBuilder.DeleteData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6cbab21-3ac7-41d4-9248-ad788c440350");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c55bedfc-9833-4973-8582-983cbbc0d4fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6490579-2f9d-4b05-9398-8929cdf0bba9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18e765a4-71fe-44c7-9494-ef3736a0b231");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ad3d737-1557-4194-9d1a-e8cee0cd8e43");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f189f16-f2c9-4525-8e1e-e85d09acfb1b");

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
                values: new object[] { 3, "789 Oak St", "Shop C", "0345678901" });
        }
    }
}
