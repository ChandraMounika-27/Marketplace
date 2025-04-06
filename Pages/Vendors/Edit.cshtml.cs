using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiVendorMarketplaceMVC.Data;
using MultiVendorMarketplaceMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiVendorMarketplaceMVC.Pages.Vendors
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vendor Vendor { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);

            if (Vendor == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Vendor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
