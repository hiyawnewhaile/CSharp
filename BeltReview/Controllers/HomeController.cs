using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeltReview.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BeltReview.Controllers;

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
        User? userInDb =_context.Users.Include(s => s.SongsWritten).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedIn = userInDb;
        ViewBag.Top = _context.Songs.Include(u => u.Artist).Include(u => u.UsersWholiked).OrderByDescending(o => o.UsersWholiked.Count).Take(3).ToList();
        return View();
    }

    [HttpGet("song/create")]
    public IActionResult AddSong()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        return View();
    }

    [HttpPost("song/add")]
    public IActionResult CreateSong(Song newSong)
    {
        if(ModelState.IsValid)
        {
            newSong.UserId = (int)HttpContext.Session.GetInt32("userId");
            _context.Add(newSong);
            _context.SaveChanges();
            return Redirect($"/song/{newSong.SongId}");
        }
        return View("AddSong");
    }

    [HttpGet("song/all")]
    public IActionResult AllSongs()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        ViewBag.AllSongs = _context.Songs.Include(a => a.Artist).Include(u => u.UsersWholiked).ToList();
        return View();
    }

    [HttpGet("song/{songId}")]
    public IActionResult OneSong(int songId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Song? SongToDisplay = _context.Songs.Include(a => a.Artist).Include(u => u.UsersWholiked).FirstOrDefault(s => s.SongId == songId);
        if(SongToDisplay == null)
        {
            return RedirectToAction("AllSongs");
        }
        return View(SongToDisplay);
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet("song/delete/{songId}")]
    public IActionResult DeleteSong(int songId)
    {
        Song? SongToDelete = _context.Songs.FirstOrDefault(s => s.SongId == songId);
        if(SongToDelete == null)
        {
            return RedirectToAction("Dashboard");
        }
        if(SongToDelete.UserId != HttpContext.Session.GetInt32("userId"))
        {
            return RedirectToAction("Logout");
        }
        _context.Songs.Remove(SongToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("song/like/{songId}")]
    public IActionResult LikeSong(int songId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Like newlike = new Like()
        {
            UserId = (int)HttpContext.Session.GetInt32("userId"),
            SongId = songId
        };
        _context.Likes.Add(newlike);
        _context.SaveChanges();

        return Redirect($"/song/{songId}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
