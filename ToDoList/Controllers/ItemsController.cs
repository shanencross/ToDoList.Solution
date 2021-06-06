using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;


namespace ToDoList.Controllers
{
  [Authorize]
  public class ItemsController : Controller
  {
    private readonly ToDoListContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ItemsController(UserManager<ApplicationUser> userManager, ToDoListContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userItems = _db.Items.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userItems);
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Item item, int categoryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      item.User = currentUser;
      _db.Items.Add(item);
      _db.SaveChanges();
      if (categoryId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId=categoryId, ItemId=item.ItemId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisItem = _db.Items
        .Include(item => item.JoinEntities)
        .ThenInclude(join => join.Category)
        .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit(Item item, int categoryId)
    {
      if (categoryId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId = categoryId, ItemId = item.ItemId });
      }
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult AddCategory(Item item, int categoryId)
    {
      if (categoryId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId = categoryId, ItemId = item.ItemId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteCategory (int joinId)
    {
      var joinEntry = _db.CategoryItem.FirstOrDefault(entry => entry.CategoryItemId == joinId);
      _db.CategoryItem.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}