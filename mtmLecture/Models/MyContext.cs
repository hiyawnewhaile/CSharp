#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace mtmLecture.Models;

public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<Actor> Actors { get; set; } 
    public DbSet<Movie> Movies { get; set; } 
    public DbSet<CastList> CastLists { get; set; } 
}