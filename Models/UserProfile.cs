using System.ComponentModel.DataAnnotations;

namespace stackoverblow.Models;

public class UserProfile
{
    public int UserProfileId { get; set; }
    [Required]
    public string Nickname { get; set; }
    public DateTimeOffset CreatedAt { get; set; } // backing-field property candidate
    [Required]
    public DateTimeOffset LastSeen { get; set; }
    public 

}
