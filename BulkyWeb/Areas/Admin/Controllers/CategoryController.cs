using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();

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

        Category fromDb = _unitOfWork.Category.Get(category => category.Id == id);

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
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
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

        Category fromDb = _unitOfWork.Category.Get(category => category.Id == id);
        ;

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

        Category fromDb = _unitOfWork.Category.Get(category => category.Id == id);
        ;

        if (fromDb == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(fromDb);
        _unitOfWork.Save();

        TempData["Success"] = "Category Deleted!";

        return RedirectToAction("Index");
    }
}