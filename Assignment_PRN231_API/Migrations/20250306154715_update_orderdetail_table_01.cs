using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class update_orderdetail_table_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TableOrders_TableId",
                table: "TableOrders");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_IngredientId",
                table: "Inventories");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1cefdb69-5a92-4373-b7a2-ac749381f13f", "23b2ca44-0cb7-4995-92c0-70729727b98f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "760b207e-7498-4fee-a596-59db41fcbcad", "46cfd47c-3e70-4019-8ab2-f56be21da33d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d4be3786-6b66-49ac-a93f-04fa1a9b0907", "7151b7d6-c6b2-4893-a875-6d26a33aaabf" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cefdb69-5a92-4373-b7a2-ac749381f13f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "760b207e-7498-4fee-a596-59db41fcbcad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4be3786-6b66-49ac-a93f-04fa1a9b0907");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23b2ca44-0cb7-4995-92c0-70729727b98f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "46cfd47c-3e70-4019-8ab2-f56be21da33d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7151b7d6-c6b2-4893-a875-6d26a33aaabf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableOrders",
                table: "TableOrders",
                columns: new[] { "TableId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                columns: new[] { "IngredientId", "ShopId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5441e72f-bf3d-43bf-ad1e-e7ca56d5486e", "f1fa55d4-be1d-4153-90de-606bb850fb48", "Manager", "MANAGER" },
                    { "7924a1ba-668f-42a6-bcb7-4cd77d86683d", "3b3e3f80-1a59-4453-8f0e-1087cd410c64", "Staff", "STAFF" },
                    { "fb633c02-7461-416b-b03a-b8e08cf51822", "fd6b87be-17a3-4868-938a-3754d354bca2", "Owner", "OWNER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Avatar", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNo", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "18ad2fb5-12fd-488d-95a6-166fd976c526", 0, 40, null, new DateTime(1984, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "074d3d34-55cb-4c06-8b1d-fd2a7f2f97d1", "owner1@example.com", true, "Alice", "Nguyen", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAEAACcQAAAAECn9MPDfdBjpzO1WL3nksVIh/7t37YAI6R+p+PqAfc29vqY9PdoxoKudFo+28SwN0A==", null, null, false, null, null, "3d6a274c-7117-4bce-979f-82108c946d5e", null, false, "owner1" },
                    { "d04fdf2d-243e-4720-be3d-6d9c07e4a3df", 0, 35, null, new DateTime(1989, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "dd1c6b08-5579-4e88-8bcf-57e589496e6d", "manager1@example.com", true, "Charlie", "Pham", false, null, "MANAGER1@EXAMPLE.COM", "MANAGER1", "AQAAAAEAACcQAAAAEBTU7xJ8nf1jn/Ut4zfwXsa7Dsq6pKLNvxmNWbt/WYNI+8gF6aBg1ypw1gGMAOMsZA==", null, null, false, null, null, "b5939383-3e88-40ac-92df-719b300623e8", null, false, "manager1" },
                    { "e151c6fa-e91f-49fe-9a9a-a1bf373983e6", 0, 28, null, new DateTime(1996, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "00a8764a-d41f-4005-bd85-3607d8887f6f", "staff1@example.com", true, "Bob", "Tran", false, null, "STAFF1@EXAMPLE.COM", "STAFF1", "AQAAAAEAACcQAAAAEBBw1Z2wT6Z0zeumJTb/KFTe7fYdrSnxll85AdbFZiklmk75DtRJpG45kRyPcagfIw==", null, null, false, null, null, "f2c2ac3a-57d8-44ca-94c3-0aa86331d583", null, false, "staff1" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "fb633c02-7461-416b-b03a-b8e08cf51822", "18ad2fb5-12fd-488d-95a6-166fd976c526" },
                    { "5441e72f-bf3d-43bf-ad1e-e7ca56d5486e", "d04fdf2d-243e-4720-be3d-6d9c07e4a3df" },
                    { "7924a1ba-668f-42a6-bcb7-4cd77d86683d", "e151c6fa-e91f-49fe-9a9a-a1bf373983e6" }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2025, 3, 3, 15, 47, 13, 834, DateTimeKind.Utc).AddTicks(3107), "e151c6fa-e91f-49fe-9a9a-a1bf373983e6" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2025, 3, 4, 15, 47, 13, 834, DateTimeKind.Utc).AddTicks(3114), "e151c6fa-e91f-49fe-9a9a-a1bf373983e6" });

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -2,
                column: "UserId",
                value: "d04fdf2d-243e-4720-be3d-6d9c07e4a3df");

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -1,
                column: "UserId",
                value: "d04fdf2d-243e-4720-be3d-6d9c07e4a3df");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TableOrders",
                table: "TableOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fb633c02-7461-416b-b03a-b8e08cf51822", "18ad2fb5-12fd-488d-95a6-166fd976c526" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5441e72f-bf3d-43bf-ad1e-e7ca56d5486e", "d04fdf2d-243e-4720-be3d-6d9c07e4a3df" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7924a1ba-668f-42a6-bcb7-4cd77d86683d", "e151c6fa-e91f-49fe-9a9a-a1bf373983e6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5441e72f-bf3d-43bf-ad1e-e7ca56d5486e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7924a1ba-668f-42a6-bcb7-4cd77d86683d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb633c02-7461-416b-b03a-b8e08cf51822");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18ad2fb5-12fd-488d-95a6-166fd976c526");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d04fdf2d-243e-4720-be3d-6d9c07e4a3df");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e151c6fa-e91f-49fe-9a9a-a1bf373983e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1cefdb69-5a92-4373-b7a2-ac749381f13f", "1ff122cf-b432-4ba5-bb78-314c8a702993", "Manager", "MANAGER" },
                    { "760b207e-7498-4fee-a596-59db41fcbcad", "e5312a71-7423-4050-9572-e1f792e52b04", "Owner", "OWNER" },
                    { "d4be3786-6b66-49ac-a93f-04fa1a9b0907", "d3c5438b-1cb6-4ef9-9f0c-21288d837cc0", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Avatar", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNo", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "23b2ca44-0cb7-4995-92c0-70729727b98f", 0, 35, null, new DateTime(1989, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "5c319e30-53c1-4ea5-b265-c4e69841ff92", "manager1@example.com", true, "Charlie", "Pham", false, null, "MANAGER1@EXAMPLE.COM", "MANAGER1", "AQAAAAEAACcQAAAAEL8BJtazV3LH+qts40oTzjO9gUoxPs7gg3J6KqAedCZs2QyH6VD9zBO7gZuHy6oR1g==", null, null, false, null, null, "32b5a5ca-bae9-4d91-9003-48f66efa6ca7", null, false, "manager1" },
                    { "46cfd47c-3e70-4019-8ab2-f56be21da33d", 0, 40, null, new DateTime(1984, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "4c247458-3b39-404f-9d02-f9665eeb51c1", "owner1@example.com", true, "Alice", "Nguyen", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAEAACcQAAAAEBZ2RVO/L7xKvHeky7mkamf4addxepWUQPWUz4PAxtblb9/XcktPu5fnJIYdh/1Lng==", null, null, false, null, null, "ef6a05c8-3e14-4e91-8ecc-5246e28f1d07", null, false, "owner1" },
                    { "7151b7d6-c6b2-4893-a875-6d26a33aaabf", 0, 28, null, new DateTime(1996, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ed94f8d0-6b77-42cd-bd8e-2cbc1aba90c4", "staff1@example.com", true, "Bob", "Tran", false, null, "STAFF1@EXAMPLE.COM", "STAFF1", "AQAAAAEAACcQAAAAEMHdYlMBVLgoYIyPWBsrJGkpRB/k1pMN/bbemznOz7x7HkEHoygRK0hxbIMxRmEnOw==", null, null, false, null, null, "b0d1f4f1-b207-4273-a25c-70ef310d6936", null, false, "staff1" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1cefdb69-5a92-4373-b7a2-ac749381f13f", "23b2ca44-0cb7-4995-92c0-70729727b98f" },
                    { "760b207e-7498-4fee-a596-59db41fcbcad", "46cfd47c-3e70-4019-8ab2-f56be21da33d" },
                    { "d4be3786-6b66-49ac-a93f-04fa1a9b0907", "7151b7d6-c6b2-4893-a875-6d26a33aaabf" }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2025, 3, 3, 14, 46, 20, 414, DateTimeKind.Utc).AddTicks(2916), "7151b7d6-c6b2-4893-a875-6d26a33aaabf" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2025, 3, 4, 14, 46, 20, 414, DateTimeKind.Utc).AddTicks(2922), "7151b7d6-c6b2-4893-a875-6d26a33aaabf" });

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -2,
                column: "UserId",
                value: "23b2ca44-0cb7-4995-92c0-70729727b98f");

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -1,
                column: "UserId",
                value: "23b2ca44-0cb7-4995-92c0-70729727b98f");

            migrationBuilder.CreateIndex(
                name: "IX_TableOrders_TableId",
                table: "TableOrders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_IngredientId",
                table: "Inventories",
                column: "IngredientId");
        }
    }
}
