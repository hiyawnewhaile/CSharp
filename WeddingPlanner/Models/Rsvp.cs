#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;

public class Rsvp
{
    [Key]
    public int RsvpId {get;set;}
    public int UserId {get;set;}
    public User? UserWhoRsvped {get;set;}
    public int WeddingId {get;set;}
    public Wedding? WeddingRsvped {get;set;}
}