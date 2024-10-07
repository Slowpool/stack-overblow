namespace StackOverblowApp.Models;

public class UserProfileModel
{
    public string Nickname { get; set; }
    public int MemberYearsCount { get; set; }
    public int MemberMonthsCount { get; set; }
    public DateTimeOffset LastSeen { get; set; }
    public string? Link { get; set; }
    public string? Country { get; set; }
    public int AnswersCount { get; set; }
    public int QuestionsCount { get; set; }
    public int Reputation { get; set; }
    public string? Description { get; set; }
}
