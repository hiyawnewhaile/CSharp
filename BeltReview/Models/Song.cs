#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltReview.Models;

public class Song
{
    [Key]
    public int SongId {get;set;}
    [Required]
    [MinLength(2)]
    public string Title {get;set;}
    [Required]
    [Range(0,59)]
    public int DurMin {get;set;}
    [Required]
    [Range(0,59)]
    public int DurSec {get;set;}
    [Required]
    public string Genre {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public int UserId {get;set;}
    public User? Artist {get;set;}
    public List<Like> UsersWholiked {get;set;} = new List<Like>();
}