#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace ProductsNCatagories.Models;

public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products { get; set; } 
    public DbSet<Category> Categories { get; set; } 
    public DbSet<Categorization> Categorizations { get; set; } 
}