using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiVendorMarketplaceMVC.Models;
using MultiVendorMarketplaceMVC.Data;
using System.Threading.Tasks;

namespace MultiVendorMarketplaceMVC.Pages.Orders
{
    public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
       _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    [BindProperty]
    public Order Order { get; set; } = new Order();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Order = await _context.Orders.FindAsync(id);
        if (Order == null)
        {
            return NotFound();
        }

        await _context.Entry(Order).Reference(o => o.Product).LoadAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}

}
