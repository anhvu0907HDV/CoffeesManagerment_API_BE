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

            modelBuilder.Entity<Inventory>()
                .HasNoKey();  // Đánh dấu là Keyless Entity
            modelBuilder.Entity<TableOrder>()
                .HasNoKey();

            List<IdentityRole> roleList = new List<IdentityRole>() { 
                new IdentityRole{
                    Name = "Owner",
                    NormalizedName = "OWNER"
                },
                new IdentityRole{
                    Name = "Staff",
                    NormalizedName = "STAFF"
                },
                new IdentityRole{
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roleList);
        }
    }
}