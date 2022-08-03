using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsNCatagories.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductsNCatagories.Controllers;

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
        ViewBag.AllProducts = _context.Products.ToList();
        return View();
    }

    [HttpPost("product/add")]
    public IActionResult AddProduct(Product newProduct)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.AllProducts = _context.Products.ToList();
        return View("Index");
    }

    [HttpGet("Categories")]
    public IActionResult AllCategories()
    {
        ViewBag.AllCategories = _context.Categories.ToList();
        return View();
    }

    [HttpPost("category/add")]
    public IActionResult AddCategory(Category newCategory)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("AllCategories");
        }
        ViewBag.AllCategories = _context.Categories.ToList();
        return View("AllCategories");
    }

    [HttpGet("prodtocat/{productId}")]
    public IActionResult ProdToCat(int productId)
    {
        ViewBag.ViewProd = _context.Products.Include(c => c.CategoryBelongingTo).ThenInclude(d => d.Category).FirstOrDefault(p => p.ProductId == productId);

        ViewBag.AdbleCat = _context.Categories.Include(c => c.ProductsBelongingToCatagory).Where(b => b.ProductsBelongingToCatagory.All(g => g.ProductId != productId)).ToList();
        return View();
    }

    [HttpPost("addassoc")]
    public IActionResult CatAssoc(Categorization newCat)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newCat);
            _context.SaveChanges();
            return Redirect($"prodtocat/{newCat.ProductId}");
        }
        return View("ProdToCat");
    }

    [HttpGet("catforprod/{CategoryId}")]
    public IActionResult CatForProd(int categoryId)
    {
        ViewBag.ViewCat = _context.Categories.Include(a => a.ProductsBelongingToCatagory).ThenInclude(b => b.Product).FirstOrDefault(c => c.CategoryId == categoryId);

        ViewBag.AdbleProd = _context.Products.Include(a => a.CategoryBelongingTo).Where(b => b.CategoryBelongingTo.All(c => c.CategoryId != categoryId)).ToList();
        return View();
    }

    [HttpPost("addprodassoc")]
    public IActionResult ProdAssoc(Categorization newProd)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newProd);
            _context.SaveChanges();
            return Redirect($"catforprod/{newProd.CategoryId}");
        }
        return View("CatForProd");
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
