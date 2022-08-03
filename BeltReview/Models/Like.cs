#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltReview.Models;

public class Like
{
    [Key]
    public int LikeId {get;set;}
    public int UserId {get;set;}
    public User? UserWhoLiked {get;set;}
    public int SongId {get;set;}
    public Song? SongLiked {get;set;}
}