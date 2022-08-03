#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mtmLecture.Models;

public class Actor
{
    [Key]
    public int  ActorId {get;set;}
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<CastList> MoviesActedIn {get;set;} = new List<CastList>();
}
