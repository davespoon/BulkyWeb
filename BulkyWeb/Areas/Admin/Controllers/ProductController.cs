using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;


    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var objCategoryList = _unitOfWork.Product.GetAll().ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm] Product obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();

            TempData["Success"] = "Product created!";
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

        Product fromDb = _unitOfWork.Product.Get(p => p.Id == id);

        if (fromDb == null)
        {
            return NotFound();
        }


        return View(fromDb);
    }

    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product Updated!";
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

        Product fromDb = _unitOfWork.Product.Get(p => p.Id == id);
       

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

        Product fromDb = _unitOfWork.Product.Get(p => p.Id == id);
        ;

        if (fromDb == null)
        {
            return NotFound();
        }

        _unitOfWork.Product.Remove(fromDb);
        _unitOfWork.Save();

        TempData["Success"] = "Product Deleted!";

        return RedirectToAction("Index");
    }
}