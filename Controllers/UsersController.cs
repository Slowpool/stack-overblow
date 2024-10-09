using DataLayer;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using StackOverblowApp.Models.Users;

namespace StackOverblowApp.Controllers;
public class UsersController : Controller
{
    private readonly AppDbContext _context;
    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int id)
    {
        var service = new UsersService(_context);
        var userProfile = service.GetUser(id);
        if (userProfile == null)
            return BadRequest("User with such an ID was not found");

        var memberYearsCount = (DateTimeOffset.Now.Year - userProfile.CreatedAt.Year - 1) +
            (DateTimeOffset.Now.Month > userProfile.CreatedAt.Month
            || (DateTimeOffset.Now.Month == userProfile.CreatedAt.Month &&
                DateTimeOffset.Now.Day > userProfile.CreatedAt.Day)
            ? 1 : 0);

        int monthsDifference = DateTimeOffset.Now.Month - userProfile.CreatedAt.Month;
        var memberMonthsCount = monthsDifference >= 0 ? monthsDifference : 0;

        return View(new UserProfileModel
        {
            Nickname = userProfile.Nickname,
            AnswersCount = userProfile.Answers.Count,
            Country = userProfile.Country?.Name,
            Description = userProfile.Description,
            LastSeen = userProfile.LastSeen,
            Link = userProfile.Link,
            MemberYearsCount = memberYearsCount,
            MemberMonthsCount = memberMonthsCount,
            QuestionsCount = userProfile.Questions.Count,
            Reputation = userProfile.Reputation
        });
    }
}
