using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.AllDishes = _context.Dishes.OrderBy(a => a.CreatedAt).ToList();
        return View();
    }

    [HttpGet("new")]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost("new/add")]
    public IActionResult AddNew(Dish newDish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.AllDishes = _context.Dishes.OrderBy(a => a.CreatedAt).ToList();
            return View("Index");
        }
    }

    [HttpGet("view/{dishID}")]
    public IActionResult View(int dishId)
    {
        Dish itemToView = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
        return View(itemToView);
    }

    [HttpGet("view/remove/{dishID}")]
    public IActionResult Delete(int dishId)
    {
        Dish dishToDelete = _context.Dishes.SingleOrDefault(a => a.DishId == dishId);
        _context.Dishes.Remove(dishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("view/editview/{dishId}")]
    public IActionResult EditView(int dishId)
    {
        Dish dishToEdit = _context.Dishes.FirstOrDefault(b => b.DishId == dishId);
        return View(dishToEdit);
    }

    [HttpPost("view/update/{dishId}")]
    public IActionResult Update(int dishId, Dish updatedDish)
    {
        Dish oldDish = _context.Dishes.FirstOrDefault(c => c.DishId == dishId);
        if(ModelState.IsValid)
        {
        oldDish.Chef = updatedDish.Chef;
        oldDish.Name = updatedDish.Name;
        oldDish.Calories = updatedDish.Calories;
        oldDish.Tastiness = updatedDish.Tastiness;
        oldDish.Description = updatedDish.Description;
        oldDish.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("View", oldDish);
        }
        return View("editview",oldDish);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
