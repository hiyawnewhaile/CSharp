#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsNCatagories.Models;

public class Category 
{
    [Key]
    public int CategoryId {get;set;}
    [Required]
    public string Name {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UdpatedAt {get;set;}
    public List<Categorization> ProductsBelongingToCatagory {get;set;} = new List<Categorization>();
}