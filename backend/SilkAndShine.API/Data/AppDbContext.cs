using Microsoft.EntityFrameworkCore;
using SilkAndShineAPI.Models;

namespace SilkAndShineAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Wishlist> Wishlists{ get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<HeroBanner> HeroBanners { get; set; }

    public DbSet<Category> Categories { get; set; }

}