using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mtmLecture.Models;
using Microsoft. EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace mtmLecture.Controllers;

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
        ViewBag.AllActors = _context.Actors.Include(c => c.MoviesActedIn).ThenInclude(m => m.Movie).ToList();
        return View();
    }

    [HttpPost("actor/add")]
    public IActionResult AddActor(Actor newActor)
    {
        if(ModelState.IsValid){
            _context.Add(newActor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.AllActors = _context.Actors.Include(c => c.MoviesActedIn).ThenInclude(m => m.Movie).ToList();
        return View("Index");
    }

    [HttpGet("movies")]
    public IActionResult AllMovies()
    {
        ViewBag.AllMovies = _context.Movies.Include(c => c.ActorsInMovie).ThenInclude(a => a.Actor).ToList();
        return View("AllMovies");
    }

    [HttpPost("movie/add")]
    public IActionResult AddMovie(Movie newMovie)
    {
        if(ModelState.IsValid){
            _context.Add(newMovie);
            _context.SaveChanges();
            return RedirectToAction("AllMovies");
        }
        return View("AllMovies");
    }

    [HttpGet("AddToCast")]
    public IActionResult AddToCast()
    {
        ViewBag.AllActors = _context.Actors.ToList();
        ViewBag.AllMovies = _context.Movies.ToList();
        return View();
    }

    [HttpPost("castlist/add")]
    public IActionResult AddCast(CastList newCasting)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newCasting);
            _context.SaveChanges();
            return RedirectToAction("AddToCast");
        }
        return View("AddToCast");
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
