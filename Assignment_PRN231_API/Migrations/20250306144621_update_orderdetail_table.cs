using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class update_orderdetail_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "68b42204-a8d7-488f-b8ca-dd7be9d73427", "286711c7-a7e0-45ba-b26f-d0bae4021a3e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6dfd91db-cac9-441b-b2c0-7b1beffaef37", "ad45c654-8dba-4db3-b1f9-b52a1a519428" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "44358a28-cb00-49ef-b984-d1311344ab6b", "d88cebfd-c229-4796-9059-9d83ceafc0b6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44358a28-cb00-49ef-b984-d1311344ab6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68b42204-a8d7-488f-b8ca-dd7be9d73427");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dfd91db-cac9-441b-b2c0-7b1beffaef37");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "286711c7-a7e0-45ba-b26f-d0bae4021a3e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad45c654-8dba-4db3-b1f9-b52a1a519428");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d88cebfd-c229-4796-9059-9d83ceafc0b6");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDetails",
                type: "int",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 1,
                column: "SubTotal",
                value: 75m);

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 2,
                column: "SubTotal",
                value: 75m);

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 3,
                column: "SubTotal",
                value: 120m);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "SubTotal",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44358a28-cb00-49ef-b984-d1311344ab6b", "224558bf-c258-46a7-8551-ed182053bcd2", "Owner", "OWNER" },
                    { "68b42204-a8d7-488f-b8ca-dd7be9d73427", "76d3bdee-cd38-4aa7-a1d1-0155dbd4a686", "Staff", "STAFF" },
                    { "6dfd91db-cac9-441b-b2c0-7b1beffaef37", "5f857af1-12ae-4889-829a-42af8281ac3a", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Avatar", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNo", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "286711c7-a7e0-45ba-b26f-d0bae4021a3e", 0, 28, null, new DateTime(1996, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "5b03f103-dbdf-4c7f-9ec8-375d227132f6", "staff1@example.com", true, "Bob", "Tran", false, null, "STAFF1@EXAMPLE.COM", "STAFF1", "AQAAAAEAACcQAAAAEO0LEOceaKQ21QuZqKIqsyhqwGSs1xh6Wh+J4H0SimvM77JQwJI2DbzKdWyLR/7Uvg==", null, null, false, null, null, "bf43274c-8d3f-491c-8ee1-0fdf617cdba3", null, false, "staff1" },
                    { "ad45c654-8dba-4db3-b1f9-b52a1a519428", 0, 35, null, new DateTime(1989, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "7eddb651-ff0b-41f1-8051-33bb0a95c771", "manager1@example.com", true, "Charlie", "Pham", false, null, "MANAGER1@EXAMPLE.COM", "MANAGER1", "AQAAAAEAACcQAAAAEBMI8q/jrTSvWzko80MyQqTQUJm5t1+6LO5ktumL7ygRx4d0ghHO/5xuWEKJ0plF7g==", null, null, false, null, null, "ea44b15f-f98b-4b62-a624-66be98e179d0", null, false, "manager1" },
                    { "d88cebfd-c229-4796-9059-9d83ceafc0b6", 0, 40, null, new DateTime(1984, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "d6cd9b92-04f1-49ce-beed-1524d0fb1482", "owner1@example.com", true, "Alice", "Nguyen", false, null, "OWNER1@EXAMPLE.COM", "OWNER1", "AQAAAAEAACcQAAAAEDKtzah/vVDcBkTJ3DWYJtVCPxJ+raHYYD6aS1NNnAnY+72lSbwmFk/q+zZfGKldbQ==", null, null, false, null, null, "167023da-a912-4f89-94d9-9fb537978eac", null, false, "owner1" }
                });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 1,
                column: "SubTotal",
                value: "75.00");

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 2,
                column: "SubTotal",
                value: "75.00");

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 3,
                column: "SubTotal",
                value: "120.00");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "68b42204-a8d7-488f-b8ca-dd7be9d73427", "286711c7-a7e0-45ba-b26f-d0bae4021a3e" },
                    { "6dfd91db-cac9-441b-b2c0-7b1beffaef37", "ad45c654-8dba-4db3-b1f9-b52a1a519428" },
                    { "44358a28-cb00-49ef-b984-d1311344ab6b", "d88cebfd-c229-4796-9059-9d83ceafc0b6" }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2025, 3, 1, 23, 20, 3, 539, DateTimeKind.Utc).AddTicks(3707), "286711c7-a7e0-45ba-b26f-d0bae4021a3e" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2025, 3, 2, 23, 20, 3, 539, DateTimeKind.Utc).AddTicks(3716), "286711c7-a7e0-45ba-b26f-d0bae4021a3e" });

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -2,
                column: "UserId",
                value: "ad45c654-8dba-4db3-b1f9-b52a1a519428");

            migrationBuilder.UpdateData(
                table: "UserShops",
                keyColumn: "Id",
                keyValue: -1,
                column: "UserId",
                value: "ad45c654-8dba-4db3-b1f9-b52a1a519428");
        }
    }
}
