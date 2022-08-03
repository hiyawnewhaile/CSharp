#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltReview.Models;

public class User
{
    [Key]
    public int UserId {get;set;}
    [Required]
    [MinLength(2)]
    public string Name {get;set;}
    [Required]
    public string Email {get;set;}
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password {get;set;}
    // Anything under the 'notMapped' will not go into the database
    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Required]
    public string PassConfirm {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<Song> SongsWritten {get;set;} = new List<Song>();
    public List<Like> SongsLiked {get;set;} = new List<Like>();
}