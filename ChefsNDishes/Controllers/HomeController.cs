using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft. EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ChefsNDishes.Controllers;

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
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View();
    }

    [HttpGet("chef/new")]
    public IActionResult AddChef()
    {
        return View();
    }

    [HttpPost("chef/new/add")]
    public IActionResult PostChef(Chef newChef)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("AddChef");
    } 

    [HttpGet("dishes")]
    public IActionResult AllDishes()
    {
        ViewBag.AllDishes = _context.Dishes.Include(c => c.Chef).ToList();
        return View();
    }

    [HttpGet("dishes/new")]
    public IActionResult AddDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View();
    }

    [HttpPost("dish/new/add")]
    public IActionResult PostDish(Dish newDish)
    {
        if(ModelState.IsValid){
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("AllDishes");
        }
        ViewBag.AllChefs = _context.Chefs.ToList();
        ViewBag.AllDishes = _context.Dishes.ToList();
        return View("AddDish");
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
