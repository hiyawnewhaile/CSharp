using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Loginreg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Loginreg.Controllers;

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

    [HttpGet("login")]
    public IActionResult Login()
    {
        HttpContext.Session.Clear();
        return View();
    }

    [HttpPost("user/register")]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid)
        {
        if(_context.Users.Any(e =>e.Email == newUser.Email))
        {
            ModelState.AddModelError("Email", "Email already in use");
            return View("Index");
        }
        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
        _context.Add(newUser);
        _context.SaveChanges();
        HttpContext.Session.SetInt32("user", newUser.UserId);
        return RedirectToAction("success");
        }
        return View("Index");
    }

    [HttpPost("user/login")]
    public IActionResult UserLogin(LogUser logInUser)
    {
        if(ModelState.IsValid)
        {
            Console.Write("Hey model is valid");
            User userInDb = _context.Users.FirstOrDefault(e => e.Email == logInUser.LogEmail);
            if(userInDb == null)
            {
                System.Console.WriteLine("Email is not in DB");
                ModelState.AddModelError("LogEmail", "Invalid Login");
                return View("Login"); 
            }
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(logInUser, userInDb.Password, logInUser.LogPassword);

            if(result == 0)
            {
                System.Console.WriteLine("Password incorrect");
                ModelState.AddModelError("LogEmail", "Invalid Login");
                return View("Login");
            }
            HttpContext.Session.SetInt32("user", userInDb.UserId);
            return RedirectToAction("success");
        }
        System.Console.WriteLine("model not valid");
        return View("Login");
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
