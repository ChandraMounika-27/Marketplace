using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiVendorMarketplaceMVC.Models;  // Add this line to reference your models
namespace MultiVendorMarketplaceMVC.Models;

public class Vendor
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;
}
