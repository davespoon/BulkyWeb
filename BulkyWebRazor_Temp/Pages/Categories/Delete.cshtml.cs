using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        [BindProperty] public Category Category { get; set; }

        public DeleteModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public void OnGet(int id)
        {
            Category = _appDbContext.Categories.Find(id);
        }

        public IActionResult OnPost()
        {
            var obj = Category;

            _appDbContext.Categories.Remove(obj);
            _appDbContext.SaveChanges();

            return RedirectToPage("Index"); 
        }
    }
}