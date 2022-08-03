#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mtmLecture.Models;

public class CastList
{
    [Key]
    public int CastListId {get;set;}
    // The connection to the Actor table
    public int ActorId {get;set;}
    public Actor? Actor {get;set;}
    // The connection to the Movie table
    public int MovieId {get;set;}
    public Movie? Movie {get;set;}
}