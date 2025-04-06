using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiVendorMarketplaceMVC.Models;
using MultiVendorMarketplaceMVC.Data;
using System.Collections.Generic;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Order Order { get; set; }

    public IList<Product> Products { get; set; }

    public void OnGet()
    {
        Products = _context.Products.ToList(); // Load products to show in the dropdown
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Products = _context.Products.ToList(); // Reload products in case of validation failure
            return Page();
        }

        // Add the new order to the database
        _context.Orders.Add(Order);
        _context.SaveChanges();

        return RedirectToPage("/Orders/Index"); // Redirect to the order list page
    }
}
