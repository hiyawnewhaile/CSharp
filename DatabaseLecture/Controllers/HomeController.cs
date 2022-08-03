using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DatabaseLecture.Models;

namespace DatabaseLecture.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        // This establishes our connection to the database
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.AllItems = _context.Items.OrderBy(a => a.Name).ToList();
        return View();
    }

    [HttpPost("item/add")]
    public IActionResult AddItem(Item newItem)
    {
        // We add this to the database so long as it's Valid
        if(ModelState.IsValid)
        {
            // We can add to the database
            _context.Add(newItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } else
        {
            // we keep them on the page with the form and show the error messages
            ViewBag.AllItems = _context.Items.OrderBy(a => a.Name).ToList();
            return View("Index");
        } 
    }

    [HttpGet("item/remove/{ItemId}")]
    public IActionResult RemoveItem(int itemId)
    {
        Item itemToDelete = _context.Items.SingleOrDefault(b => b.ItemId == itemId);
        _context.Items.Remove(itemToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index"); 
    }

    [HttpGet("item/update/{ItemId}")]
    public IActionResult UpdateItem(int itemId)
    {
        // We need to locate the item
        Item itemToUpdate = _context.Items.FirstOrDefault(u => u.ItemId == itemId);
        return View(itemToUpdate);
    }

    [HttpPost("item/update/{ItemId}")]
    public IActionResult UpdatedItem(int itemId, Item UpdatedItem)
    {
        Item oldItem = _context.Items.FirstOrDefault(a => a.ItemId == itemId);
        // This is not a valid way to update
        // oldItem = UpdatedItem
        oldItem.Name = UpdatedItem.Name;
        oldItem.Description = UpdatedItem.Description;
        oldItem.UpdatedAt = DateTime.Now;
        _context.SaveChanges();

        return RedirectToAction("Index");
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
