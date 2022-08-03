#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace DatabaseLecture.Models;

public class Item
{
    // We need an ID
    // Make sure this is the name of your model + ID
    [Key]
    public int ItemId {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    [MinLength(10)]
    public string Description {get;set;}
    // We always include a CreatedAt and UpdatedAt bc its good practice
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}
