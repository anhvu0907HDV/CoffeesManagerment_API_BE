using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class AddRefreshTokenToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1821af7a-e3f3-4065-95dc-f632c4c14fe3", "d253582f-7b42-430a-b4ce-a9e5b66ef828", "Owner", "OWNER" },
                    { "8d58b673-333d-486e-94b1-2c021a1b0799", "377afd48-fc46-4890-93a9-989be9915631", "Staff", "STAFF" },
                    { "d3ab3a31-1935-4f83-a8c3-4a4c2c4a7c20", "da0625fa-7208-4372-9803-19e556df20d7", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Avatar", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNo", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "24f9d0a3-cdc8-4a4a-831d-6c586be271ab", 0, 40, null, new DateTime(1984, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "22ad87b4-a3c4-4648-880c-15deea17c571", "owner1@example.com", true, "Alice", "Nguyen", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAEAACcQAAAAEEkFmuDp1oMq+cAraO3QFPSS1TlmxH6tqn/LZOrEmZsUFWOg9LIFWlmrmJeHFnhSUQ==", null, null, false, null, null, "69aca8ed-cc8f-4561-9df1-aeeccc97ae3d", null, false, "owner1" },
                    { "70db24bd-de74-4c31-bb13-b04590c4e6f2", 0, 35, null, new DateTime(1989, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0cd85690-dad0-451d-aabe-51d1b0b9ea3c", "manager1@example.com", true, "Charlie", "Pham", false, null, "MANAGER1@EXAMPLE.COM", "MANAGER1", "AQAAAAEAACcQAAAAEMI05kc8zE8aw1ZW7dGws/krHwteVLmgnQbEyVwsDlFvWGDf5om1bd5EXgv9bzUrLg==", null, null, false, null, null, "41cc59ec-b96d-45c2-b16c-e164b6961638", null, false, "manager1" },
                    { "93f959f1-55c8-443d-97ec-963f5eccca4a", 0, 28, null, new DateTime(1996, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "b526edb9-42ac-4249-a43e-04c2bb5fc941", "staff1@example.com", true, "Bob", "Tran", false, null, "STAFF1@EXAMPLE.COM", "STAFF1", "AQAAAAEAACcQAAAAEG0WjL2SrwcLTFZQcc4zQgU9CHEg+fruTZr0MwueiODHmnjA/llpi8vlS1zgATkQfg==", null, null, false, null, null, "04657a91-bb8e-4948-aa1e-4bccb55c0a9d", null, false, "staff1" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1821af7a-e3f3-4065-95dc-f632c4c14fe3", "24f9d0a3-cdc8-4a4a-831d-6c586be271ab" },
                    { "d3ab3a31-1935-4f83-a8c3-4a4c2c4a7c20", "70db24bd-de74-4c31-bb13-b04590c4e6f2" },
                    { "8d58b673-333d-486e-94b1-2c021a1b0799", "93f959f1-55c8-443d-97ec-963f5eccca4a" }
                });

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -2,
                column: "UserId",
                value: "70db24bd-de74-4c31-bb13-b04590c4e6f2");

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -1,
                column: "UserId",
                value: "70db24bd-de74-4c31-bb13-b04590c4e6f2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1821af7a-e3f3-4065-95dc-f632c4c14fe3", "24f9d0a3-cdc8-4a4a-831d-6c586be271ab" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3ab3a31-1935-4f83-a8c3-4a4c2c4a7c20", "70db24bd-de74-4c31-bb13-b04590c4e6f2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8d58b673-333d-486e-94b1-2c021a1b0799", "93f959f1-55c8-443d-97ec-963f5eccca4a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1821af7a-e3f3-4065-95dc-f632c4c14fe3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d58b673-333d-486e-94b1-2c021a1b0799");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3ab3a31-1935-4f83-a8c3-4a4c2c4a7c20");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24f9d0a3-cdc8-4a4a-831d-6c586be271ab");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70db24bd-de74-4c31-bb13-b04590c4e6f2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93f959f1-55c8-443d-97ec-963f5eccca4a");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
