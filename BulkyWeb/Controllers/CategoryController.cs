using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    // private readonly ApplicationDbContext _db;

    private readonly ICategoryRepository _repository;

    public CategoryController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var objCategoryList = _repository.GetAll().ToList();
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
            _repository.Add(obj);
            _repository.Save();

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

        Category fromDb = _repository.Get(category => category.Id == id);

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
            _repository.Update(obj);
            _repository.Save();
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

        Category fromDb = _repository.Get(category => category.Id == id);;

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

        Category fromDb = _repository.Get(category => category.Id == id);;

        if (fromDb == null)
        {
            return NotFound();
        }

        _repository.Remove(fromDb);
        _repository.Save();

        TempData["Success"] = "Category Deleted!";

        return RedirectToAction("Index");
    }
}