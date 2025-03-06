using Assignment_PRN231_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_VS.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
 
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeDetail> RecipeDetails { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;
        public virtual DbSet<TableOrder> TableOrders { get; set; } = null!;
        public virtual DbSet<UserShop> UserShops { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inventory>().HasKey(to => new { to.IngredientId, to.ShopId });
            modelBuilder.Entity<TableOrder>()
                .HasKey(to => new { to.TableId, to.OrderId });
            // 2️⃣ Seed Roles
            var ownerRoleId = Guid.NewGuid().ToString();
            var staffRoleId = Guid.NewGuid().ToString();
            var managerRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = ownerRoleId, Name = "Owner", NormalizedName = "OWNER" },
                new IdentityRole { Id = staffRoleId, Name = "Staff", NormalizedName = "STAFF" },
                new IdentityRole { Id = managerRoleId, Name = "Manager", NormalizedName = "MANAGER" }
            );

            // 3️⃣ Seed Users
            var hasher = new PasswordHasher<AppUser>();

            var ownerId = Guid.NewGuid().ToString();
            var staffId = Guid.NewGuid().ToString();
            var managerId = Guid.NewGuid().ToString();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = ownerId,
                    UserName = "owner1",
                    NormalizedUserName = "OWNER1",
                    Email = "owner1@example.com",
                    NormalizedEmail = "OWNER1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Owner@123"),
                    FirstName = "Alice",
                    LastName = "Nguyen",
                    Age = 40,
                    Birthday = new DateTime(1984, 3, 22)
                },
                new AppUser
                {
                    Id = staffId,
                    UserName = "staff1",
                    NormalizedUserName = "STAFF1",
                    Email = "staff1@example.com",
                    NormalizedEmail = "STAFF1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Staff@123"),
                    FirstName = "Bob",
                    LastName = "Tran",
                    Age = 28,
                    Birthday = new DateTime(1996, 7, 12)
                },
                new AppUser
                {
                    Id = managerId,
                    UserName = "manager1",
                    NormalizedUserName = "MANAGER1",
                    Email = "manager1@example.com",
                    NormalizedEmail = "MANAGER1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Manager@123"),
                    FirstName = "Charlie",
                    LastName = "Pham",
                    Age = 35,
                    Birthday = new DateTime(1989, 5, 15)
                }
            );

            // 4️⃣ Gán Users vào Roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = ownerId, RoleId = ownerRoleId },
                new IdentityUserRole<string> { UserId = staffId, RoleId = staffRoleId },
                new IdentityUserRole<string> { UserId = managerId, RoleId = managerRoleId }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Coffee" },
                new Category { CategoryId = 2, CategoryName = "Tea" },
                new Category { CategoryId = 3, CategoryName = "Juice" }
            );
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeId = 1,
                    ProductId = 1,
                    Description = "Classic espresso recipe"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Espresso",
                    Price = 3.5M,
                    CategoryId = 1, // ✅ Đã seed trước đó
                    RecipeId = 1,   // ✅ Để liên kết với Recipe
                    Image = "espresso.jpg",
                    Size = 250,
                    Quantity = 100,
                    IsActive = 1
                }
            );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    IngredientId = 1,
                    IngredientName = "Coffee Beans",
                    Unit = 100.0M // Gram
                }
);
            modelBuilder.Entity<RecipeDetail>().HasData(
                new RecipeDetail
                {
                    RecipeDetailId = 1,
                    RecipeId = 1, // Đảm bảo RecipeId = 1 đã tồn tại
                    IngredientId = 1, // Coffee Beans
                    Quantity = 50.0M
                }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    PaymentId = "PAY001",
                    OrderId = 1,
                    PaymentMethod = "Credit Card",
                    PaymentStatus = "Completed"
                },
                new Payment
                {
                    PaymentId = "PAY002",
                    OrderId = 2,
                    PaymentMethod = "Cash",
                    PaymentStatus = "Pending"
                }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    UserId = staffId,
                    OrderDate = DateTime.UtcNow.AddDays(-3),
                    TotalAmount = 150.00m,
                    OrderStatus = "Completed",
                    PaymentId = "PAY001"
                },
                new Order
                {
                    OrderId = 2,
                    UserId = staffId,
                    OrderDate = DateTime.UtcNow.AddDays(-2),
                    TotalAmount = 220.00m,
                    OrderStatus = "Pending",
                    PaymentId = "PAY001"
                }
            );
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    OrderDetailId = 1,
                    OrderId = 1,
                    ProductId = 1,
                    SubTotal =(decimal?) 75.00
                },
                new OrderDetail
                {
                    OrderDetailId = 2,
                    OrderId = 1,
                    ProductId = 1,
                    SubTotal = (decimal?)75.00
                },
                new OrderDetail
                {
                    OrderDetailId = 3,
                    OrderId = 2,
                    ProductId = 1,
                    SubTotal = (decimal?)120.00
                }
            );
            modelBuilder.Entity<Shop>().HasData(
                new Shop { ShopId = 1, Name = "Shop A", Address = "123 Main St", PhoneNumber = "0123456789" },
                new Shop { ShopId = 2, Name = "Shop B", Address = "456 Elm St", PhoneNumber = "0987654321" }
            );

            modelBuilder.Entity<UserShop>().HasData(
                new UserShop { Id = -1, UserId = managerId, ShopId = 1, Role = "Manager" },
                new UserShop { Id = -2, UserId = managerId, ShopId = 2, Role = "Manager" }
            );
        }
    }
}