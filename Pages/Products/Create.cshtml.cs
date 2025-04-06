public IActionResult OnPost()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    _context.Products.Add(Product);
    _context.SaveChanges();

    return RedirectToPage("/Products/Index");
}
