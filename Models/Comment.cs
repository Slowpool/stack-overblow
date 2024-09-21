using System.ComponentModel.DataAnnotations;

namespace stackoverblow.Models;

public class Comment
{
    public int CommentId { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public DateTimeOffset LeftAt { get; set; }
}
