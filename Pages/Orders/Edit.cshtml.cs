using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MultiVendorMarketplaceMVC.Data;
using MultiVendorMarketplaceMVC.Models;

namespace MultiVendorMarketplaceMVC.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order? Order { get; set; } // ✅ Nullable to avoid CS8601 warning

        public IList<Product> Products { get; set; } = new List<Product>(); // ✅ Initialize to avoid null warning

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Order = await _context.Orders
                                  .Include(o => o.Product)
                                  .FirstOrDefaultAsync(o => o.Id == id);

            if (Order == null)
            {
                return NotFound();
            }

            Products = await _context.Products.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Products = await _context.Products.ToListAsync(); // ✅ Re-load dropdown on validation error
                return Page();
            }

            _context.Attach(Order!).State = EntityState.Modified; // ✅ Use ! to indicate non-null after validation

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Orders.Any(e => e.Id == Order!.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
