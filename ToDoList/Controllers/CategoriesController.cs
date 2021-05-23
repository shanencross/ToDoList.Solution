using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly ToDoListContext _db;
    public CategoriesController(ToDoListContext db)
    {
      _db = db; 
    }

    public ActionResult Index()
    {
      List<Category> model = _db.Categories.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Description");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Category category, int itemId)
    {
      _db.Categories.Add(category);
      _db.SaveChanges();
      if (itemId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId=category.CategoryId, ItemId=itemId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCategory = _db.Categories
        .Include(category => category.JoinEntities)
        .ThenInclude(join => join.Item)
        .FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    public ActionResult Edit(int id)
    {
      Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Description");
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult Edit(Category category, int itemId)
    {      
      if (itemId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId=category.CategoryId, ItemId=itemId});
      }
      _db.Entry(category).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddItem(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Description");
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult AddItem(Category category, int itemId)
    {
      if (itemId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId=category.CategoryId, ItemId=itemId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}