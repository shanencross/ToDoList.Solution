using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;

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

//     [HttpGet("/categories")]
//     public ActionResult Index()
//     {
//       List<Category> allCategories = Category.GetAll();
//       return View(allCategories);
//     }

//     [HttpGet("/categories/new")]
//     public ActionResult New()
//     {
//       return View();
//     }

//     [HttpGet("/categories/{id}")]
//     public ActionResult Show(int id)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category selectedCategory = Category.Find(id);
//       List<Item> categoryItems = selectedCategory.Items;
//       model.Add("category", selectedCategory);
//       model.Add("items", categoryItems);
//       return View(model);
//     }

//     [HttpPost("/categories")]
//     public ActionResult Create(string categoryName)
//     {
//       Category newCategory = new Category(categoryName);
//       return RedirectToAction("Index");
//     }

//     [HttpPost("/categories/{categoryId}/items")]
//     public ActionResult Create(int categoryId, string itemDescription)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category foundCategory = Category.Find(categoryId);
//       Item newItem = new Item(itemDescription);
//       newItem.Save();
//       foundCategory.AddItem(newItem);
//       List<Item> categoryItems = foundCategory.Items;
//       model.Add("items", categoryItems);
//       model.Add("category", foundCategory);
//       return View("Show", model);
//     }
  }
}