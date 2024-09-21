using System.ComponentModel.DataAnnotations;

namespace stackoverblow.Models;

public class Question
{
    public int QuestionId { get; set; }
    [Required]
    public DateTimeOffset AskedAt { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    [Required]
    public int Views { get; set; }
    [Required]
    public int VoteCount { get; set; }
    [Required]
    public string Content { get; set; }
}
