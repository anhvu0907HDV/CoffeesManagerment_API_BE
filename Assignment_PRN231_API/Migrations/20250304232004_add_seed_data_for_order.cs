using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_PRN231_API.Migrations
{
    public partial class add_seed_data_for_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserShops_Shops_ShopId",
                table: "UserShops");

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

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "UserShops",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Coffee" },
                    { 2, "Tea" },
                    { 3, "Juice" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "IngredientName", "Unit" },
                values: new object[] { 1, "Coffee Beans", 100.0m });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "OrderId", "PaymentMethod", "PaymentStatus" },
                values: new object[,]
                {
                    { "PAY001", 1, "Credit Card", "Completed" },
                    { "PAY002", 2, "Cash", "Pending" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "68b42204-a8d7-488f-b8ca-dd7be9d73427", "286711c7-a7e0-45ba-b26f-d0bae4021a3e" },
                    { "6dfd91db-cac9-441b-b2c0-7b1beffaef37", "ad45c654-8dba-4db3-b1f9-b52a1a519428" },
                    { "44358a28-cb00-49ef-b984-d1311344ab6b", "d88cebfd-c229-4796-9059-9d83ceafc0b6" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "OrderDate", "OrderStatus", "PaymentId", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 1, 23, 20, 3, 539, DateTimeKind.Utc).AddTicks(3707), "Completed", "PAY001", 150.00m, "286711c7-a7e0-45ba-b26f-d0bae4021a3e" },
                    { 2, new DateTime(2025, 3, 2, 23, 20, 3, 539, DateTimeKind.Utc).AddTicks(3716), "Pending", "PAY001", 220.00m, "286711c7-a7e0-45ba-b26f-d0bae4021a3e" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Discount", "Image", "IsActive", "Price", "ProductName", "Quantity", "RecipeId", "Size" },
                values: new object[] { 1, 1, null, null, "espresso.jpg", 1, 3.5m, "Espresso", 100, 1, 250 });

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

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "OrderId", "ProductId", "SubTotal" },
                values: new object[,]
                {
                    { 1, 1, 1, "75.00" },
                    { 2, 1, 1, "75.00" },
                    { 3, 2, 1, "120.00" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "Description", "ProductId" },
                values: new object[] { 1, "Classic espresso recipe", 1 });

            migrationBuilder.InsertData(
                table: "RecipeDetails",
                columns: new[] { "RecipeDetailId", "IngredientId", "Quantity", "RecipeId" },
                values: new object[] { 1, 1, 50.0m, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_UserShops_Shops_ShopId",
                table: "UserShops",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserShops_Shops_ShopId",
                table: "UserShops");

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
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "PAY002");

            migrationBuilder.DeleteData(
                table: "RecipeDetails",
                keyColumn: "RecipeDetailId",
                keyValue: 1);

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
                keyValue: "ad45c654-8dba-4db3-b1f9-b52a1a519428");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d88cebfd-c229-4796-9059-9d83ceafc0b6");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "286711c7-a7e0-45ba-b26f-d0bae4021a3e");

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: "PAY001");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "UserShops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserShops_Shops_ShopId",
                table: "UserShops",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
