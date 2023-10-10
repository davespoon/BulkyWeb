using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm] Category obj)
    {
        if (obj.Name == "Bob")
        {
            ModelState.AddModelError("name", "The name could not be Bob. I don't like it. Sorry Bob!");
        }

        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();

            TempData["Success"] = "Category created!";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category fromDb = _db.Categories.Find(id);

        if (fromDb == null)
        {
            return NotFound();
        }

        // var obj = _db.Categories.FirstOrDefault(category => category.Id == id);

        return View(fromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == "Bob")
        {
            ModelState.AddModelError("name", "The name could not be Bob. I don't like it. Sorry Bob!");
        }

        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Updated!";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    public IActionResult Delete(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category fromDb = _db.Categories.Find(id);

        if (fromDb == null)
        {
            return NotFound();
        }

        return View(fromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category fromDb = _db.Categories.Find(id);

        if (fromDb == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(fromDb);
        _db.SaveChanges();

        TempData["Success"] = "Category Deleted!";

        return RedirectToAction("Index");
    }
}