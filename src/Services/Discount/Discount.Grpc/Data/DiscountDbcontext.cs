using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountDbcontext:DbContext
    {
        public DiscountDbcontext(DbContextOptions<DiscountDbcontext> options):base(options)
        {
            
        }
        public DbSet<Coupon> Coupones { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id =1, ProductName = "IPhone X", Amount = 150, Description = "Description IPhoneX"},
                new Coupon { Id = 2, ProductName = "SamSum X", Amount = 150, Description = "Description SamSum X" });
        }
    }
}
