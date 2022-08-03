using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft. EntityFrameworkCore;
using OneToManyLecture.Models;

namespace OneToManyLecture.Controllers;

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
        ViewBag.AllOwners = _context.Owners.Include(p => p.PetsOwned).ToList();
        return View();
    }

    [HttpPost("owner/add")]
    public IActionResult AddOwner(Owner newOwner)
    {
        if(ModelState.IsValid)
        {
            // we can add to the database
            _context.Add(newOwner);
            _context.SaveChanges();  
            return RedirectToAction("Index");
        }
        ViewBag.AllOwners = _context.Owners.Include(p => p.PetsOwned).ToList();
        return View("Index");
    }

    [HttpGet("pets")]
    public IActionResult Pets()
    {
        ViewBag.AllOwners = _context.Owners.ToList();
        ViewBag.AllPets = _context.Pets.Include(o => o.Owner).ToList();
        return View();
    }

    [HttpPost("pets/add")]
    public IActionResult AddPet(Pet newPet)
    {
        if(ModelState.IsValid)
        {
            // We add to database
            _context.Add(newPet);
            _context.SaveChanges();
            return RedirectToAction("pets");
        }
        ViewBag.AllOwners = _context.Owners.ToList();
        ViewBag.AllPets = _context.Pets.Include(o => o.Owner).ToList();
        return View("pets");
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
