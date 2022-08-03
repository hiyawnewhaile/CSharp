#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsNCatagories.Models;

public class Categorization
{
    [Key]
    public int CategorizationId {get;set;}
    public int ProductId {get;set;}
    public Product? Product {get;set;}
    public int CategoryId {get;set;}
    public Category? Category {get;set;}
}