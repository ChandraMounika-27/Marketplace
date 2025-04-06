using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiVendorMarketplaceMVC.Data;
using MultiVendorMarketplaceMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiVendorMarketplaceMVC.Pages.Vendors
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Vendor> Vendors { get; set; }

        public async Task OnGetAsync()
        {
            Vendors = await _context.Vendors.ToListAsync();
        }
    }
}
