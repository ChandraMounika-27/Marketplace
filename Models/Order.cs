using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiVendorMarketplaceMVC.Models;  
namespace MultiVendorMarketplaceMVC.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; }=null!;
}
