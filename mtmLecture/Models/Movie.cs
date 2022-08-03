#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mtmLecture.Models;

public class Movie
{
    [Key]
    public int MovieID {get;set;}
    public string Title {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<CastList> ActorsInMovie {get;set;} = new List<CastList>();
}