using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WeddingPlanner.Controllers;

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
        return View();
    }

    [HttpPost("user/register")]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid)
        {
            // Verify if the email is Unique
            if(_context.Users.Any(e =>e.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "Email already in use");
                return View("Index");
            }
            // Hash password
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", newUser.UserId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
    }

    [HttpPost("user/login")]
    public IActionResult Login(LogUser loginUser)
    {
        if(ModelState.IsValid)
        {
            //Verify if email is in db
            User? userInDb = _context.Users.FirstOrDefault(e => e.Email == loginUser.LogEmail);
            if(userInDb == null)
            {
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            // Verify if the password matches
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LogPassword);
            if(result == 0)
            {
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            HttpContext.Session.SetInt32("userId", userInDb.UserId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        User? userInDb =_context.Users.Include(s => s.WeddingsPosted).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedIn = userInDb;
        ViewBag.AllWeddings = _context.Weddings.Include(a => a.WeddingPlanner).Include(u => u.PeopleWhoRsvped).ToList();
        foreach(Wedding w in ViewBag.AllWeddings)
        {
        if(w.Date < DateTime.Now)
        {
            return Redirect($"wedding/delete/{w.WeddingId}");
        }
        }
        return View();
    }

    [HttpGet("wedding/add")]
    public IActionResult AddWedding()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        return View();
    }

    [HttpPost("wedding/add/post")]
    public IActionResult NewWedding(Wedding newWedding)
    {
        if(ModelState.IsValid)
        {
            newWedding.UserId = (int) HttpContext.Session.GetInt32("userId");
            if(newWedding.Date < DateTime.Now)
            {
                ModelState.AddModelError("Date", "Date must be a Future date");
                return View("AddWedding");
            }
            _context.Add(newWedding);
            _context.SaveChanges();
            return Redirect($"/onewedding/{newWedding.WeddingId}");
        }
        return View("AddWedding");
    }

    [HttpGet("onewedding/{weddingId}")]
    public IActionResult OneWedding(int weddingId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Wedding? WeddingToDisplay = _context.Weddings.Include(p => p.PeopleWhoRsvped).ThenInclude(u => u.UserWhoRsvped).FirstOrDefault(w => w.WeddingId == weddingId);
        if(WeddingToDisplay == null)
        {
            return View("Dashboard");
        }
        return View(WeddingToDisplay);
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet("wedding/delete/{weddingId}")]
    public IActionResult DeleteSong(int weddingId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Wedding? WeddingToDelete = _context.Weddings.FirstOrDefault(s => s.WeddingId == weddingId);
        if(WeddingToDelete == null)
        {
            return RedirectToAction("Dashboard");
        }
        if(WeddingToDelete.UserId != HttpContext.Session.GetInt32("userId"))
        {
            return RedirectToAction("Logout");
        }
        _context.Weddings.Remove(WeddingToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("wedding/rsvp/{weddingId}")]
    public IActionResult Rsvp(int weddingId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Rsvp newRsvp = new Rsvp()
        {
            UserId = (int)HttpContext.Session.GetInt32("userId"),
            WeddingId = weddingId
        };
        _context.Rsvps.Add(newRsvp);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("wedding/unrsvp/{weddingId}")]
    public IActionResult Unrsvp(int weddingId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Rsvp? RsvpToDelete = _context.Rsvps.FirstOrDefault(s => s.WeddingId == weddingId);
        if(RsvpToDelete == null)
        {
            return RedirectToAction("Dashboard");
        }
        if(RsvpToDelete.UserId != HttpContext.Session.GetInt32("userId"))
        {
            return RedirectToAction("Logout");
        }
        _context.Rsvps.Remove(RsvpToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
