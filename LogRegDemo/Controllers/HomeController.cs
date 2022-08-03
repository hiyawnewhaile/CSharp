using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LogRegDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LogRegDemo.Controllers;

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
        HttpContext.Session.Clear();
        return View();
    }

    [HttpPost("user/register")]
    public IActionResult Register(User newUser)
    {
        // We Passed validations
        if(ModelState.IsValid)
        {
            // We need to check if the username is unique
            if(_context.Users.Any(e =>e.Email == newUser.Email))
            {
                // Email already in use
                ModelState.AddModelError("Email", "Email already in Use!");
                return View("Index");
            }
            // If all goes well then we Hash our Password
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            // Then we store the user Id for making sure noone else can see the page
            HttpContext.Session.SetInt32("user", newUser.UserId);
            return RedirectToAction("Success");
        }
        return View("Index");
    }

    [HttpPost("user/login")]
    public IActionResult Login(LogUser loginUser)
    {
        if(ModelState.IsValid)
        {
            // Step 1: Find the email and if not found, throw error message
            User userInDb = _context.Users.FirstOrDefault(u => u.Email == loginUser.LogEmail);
            if(userInDb == null)
            {
                // There was no matching email in the DB so throw an error message
                ModelState.AddModelError("LogEmail", "Invalid Login");
                return View("index");
            }
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LogPassword);

            if(result == 0)
            {
                // This means the correct password has not been input
                ModelState.AddModelError("LogEmail", "Invalid Login");
                return View("index");
            }
            HttpContext.Session.SetInt32("user", userInDb.UserId);
            return RedirectToAction("Success");
        }
        return View("Index");
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        if(HttpContext.Session.GetInt32("user") == null)
        {
            return RedirectToAction("Index");
        }
        User loggedInUser = _context.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user"));
        return View(loggedInUser);
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
