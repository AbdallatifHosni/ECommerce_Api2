using ECommerce_Api2.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Api2.Data
{
    public class EcommerceContext:DbContext
    {
        public EcommerceContext()
        {

        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderProducts>().HasKey(u => new
            {
                u.ProductId,
                u.OrderId
            });
            modelBuilder.Entity<WishProducts>().HasKey(u => new
            {
                u.ProductId,
                u.WishListId
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishProducts> WishProducts { get; set; }
    }
}
