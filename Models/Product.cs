using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiVendorMarketplaceMVC.Models;  // Add this line to reference your models
namespace MultiVendorMarketplaceMVC.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public decimal Price { get; set; }
    public int VendorId { get; set; }

    public Vendor Vendor { get; set; }=null!;
}
