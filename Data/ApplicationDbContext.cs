using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiVendorMarketplaceMVC.Models;
using Microsoft.AspNetCore.Identity; 

namespace MultiVendorMarketplaceMVC.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Vendor> Vendors { get; set; }=default!;
    public DbSet<Product> Products { get; set; }=default!;
    public DbSet<Order> Orders { get; set; }=default!;
}
