using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories;

[BindProperties]
public class CreateModel : PageModel
{
    private readonly AppDbContext _appDbContext;
    public Category Category { get; set; }

    public CreateModel(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (Category.Name == "Bob")
        {
            ModelState.AddModelError("name", "The name could not be Bob. I don't like it. Sorry Bob!");
        }

        if (ModelState.IsValid)
        {
            _appDbContext.Categories.Add(Category);
            _appDbContext.SaveChanges();
            TempData["Success"] = "Category created!";

            return RedirectToPage("Index");
        }

        return Page();
    }
}