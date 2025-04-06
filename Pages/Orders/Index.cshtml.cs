using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiVendorMarketplaceMVC.Data;
using MultiVendorMarketplaceMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiVendorMarketplaceMVC.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }=new List<Order>();

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders.Include(o => o.Product).ToListAsync();
        }
    }
}
