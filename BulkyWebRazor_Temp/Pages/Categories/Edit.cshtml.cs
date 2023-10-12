using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        [BindProperty] public Category? Category { get; set; }

        public EditModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _appDbContext.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (Category != null)
            {
                _appDbContext.Categories.Update(Category);
                _appDbContext.SaveChanges();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}