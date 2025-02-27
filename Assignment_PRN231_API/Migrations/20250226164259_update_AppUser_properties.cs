using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class update_AppUser_properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05266884-3d85-4621-90f7-b14c0c0ee68d", "692c8b18-de34-4aa9-90c8-93e4075d9ffa", "Staff", "STAFF" },
                    { "2da92726-5cef-4815-916f-449f5c289a48", "e4c58b55-fab5-4b1c-94af-3570a9740ccc", "Manager", "MANAGER" },
                    { "571265ec-7c34-4c97-a8b2-b751ca39d9d6", "9f13cc5a-6ead-45ee-a366-883f4cadfd39", "Owner", "OWNER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Avatar", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNo", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3fa266e2-8e62-4854-93d0-e6588c5cd23f", 0, 35, null, new DateTime(1989, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "9b45f390-013d-437e-bedb-c59cb8bf4a04", "manager1@example.com", true, "Charlie", "Pham", false, null, "MANAGER1@EXAMPLE.COM", "MANAGER1", "AQAAAAEAACcQAAAAEKicFm6s/Ck5p2dasVgsXYh1vzIKmMibD1Kpy38ETTNS4kx/qOU5x3c7VSs0Dw2kgg==", null, null, false, "0adf9998-4cb6-46e5-a87f-5ee976129750", null, false, "manager1" },
                    { "5fbbcc1d-bac4-4fd8-b953-09c182f343c5", 0, 28, null, new DateTime(1996, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "8af44483-6e75-4607-b242-94be01082db3", "staff1@example.com", true, "Bob", "Tran", false, null, "STAFF1@EXAMPLE.COM", "STAFF1", "AQAAAAEAACcQAAAAEAa7HKUxkQ3f01lc5E3iRu58oAqmVFBDRxoh4EmDz5OMD/GopIwAED9QT22ZF9m11Q==", null, null, false, "ef6c908e-67cf-4439-b6f0-698979196e6c", null, false, "staff1" },
                    { "facefd75-750f-4ed9-80af-7eb38354f114", 0, 40, null, new DateTime(1984, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "2de49a32-cb5f-4afa-a84c-0dea1f8e938c", "owner1@example.com", true, "Alice", "Nguyen", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAEAACcQAAAAEHHmh2upR+JJKs8hcP/swCkFspIhE02Rq/RUOnkIQtNMiU+DNG2FBuHPgYM+ZvIQCg==", null, null, false, "9efc65a0-5e42-4a7a-9f05-ebc91cac82d0", null, false, "owner1" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2da92726-5cef-4815-916f-449f5c289a48", "3fa266e2-8e62-4854-93d0-e6588c5cd23f" },
                    { "05266884-3d85-4621-90f7-b14c0c0ee68d", "5fbbcc1d-bac4-4fd8-b953-09c182f343c5" },
                    { "571265ec-7c34-4c97-a8b2-b751ca39d9d6", "facefd75-750f-4ed9-80af-7eb38354f114" }
                });

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -2,
                column: "UserId",
                value: "3fa266e2-8e62-4854-93d0-e6588c5cd23f");

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -1,
                column: "UserId",
                value: "3fa266e2-8e62-4854-93d0-e6588c5cd23f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2da92726-5cef-4815-916f-449f5c289a48", "3fa266e2-8e62-4854-93d0-e6588c5cd23f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "05266884-3d85-4621-90f7-b14c0c0ee68d", "5fbbcc1d-bac4-4fd8-b953-09c182f343c5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "571265ec-7c34-4c97-a8b2-b751ca39d9d6", "facefd75-750f-4ed9-80af-7eb38354f114" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05266884-3d85-4621-90f7-b14c0c0ee68d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2da92726-5cef-4815-916f-449f5c289a48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "571265ec-7c34-4c97-a8b2-b751ca39d9d6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3fa266e2-8e62-4854-93d0-e6588c5cd23f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5fbbcc1d-bac4-4fd8-b953-09c182f343c5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "facefd75-750f-4ed9-80af-7eb38354f114");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "AspNetUsers");

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

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -2,
                column: "UserId",
                value: "9f189f16-f2c9-4525-8e1e-e85d09acfb1b");

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -1,
                column: "UserId",
                value: "9f189f16-f2c9-4525-8e1e-e85d09acfb1b");
        }
    }
}
