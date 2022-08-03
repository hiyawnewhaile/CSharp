#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneToManyLecture.Models;

public class Pet
{
    [Key]
    public int PetId {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public string Species {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public int OwnerId {get;set;}
    // Navigation property that lets us see the whole owner
    public Owner? Owner {get;set;}
}