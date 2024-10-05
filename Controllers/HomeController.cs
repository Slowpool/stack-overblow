using System.Diagnostics;
using DataLayer;
using DataLayer.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;
using StackOverblowApp.Models;

namespace StackOverblowApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet(template: "/search", Name = "GetQuestions")]
    // https://localhost:7296/search?q=nice&page=1&orderBy=Newest
    public async Task<IActionResult> Search([FromQuery] int page, [FromQuery] string q, [FromQuery] string orderBy)
    {
        if (q.Length == 0)
            return View();

        if (!Enum.TryParse(orderBy, true, out QuestionsOrderBy orderByOption))
        {
            return View();
        }

        var options = new QuestionResearchOptions(orderByOption, page);
        var service = new QuestionService(_context);
        var questions = service.FindQuestions(q, options);

        var questionsModel = new List<ResearchQuestionModel>();
        string authorAvatar;
        string authorNickname;
        int authorReputation;
        foreach (var question in questions)
        {
            if (question.AskingUser != null)
            {
                authorAvatar = question.AskingUser.Avatar;
                authorNickname = question.AskingUser.Nickname;
                authorReputation = question.AskingUser.Reputation;
            }
            else
            {
                authorAvatar = "default pic";
                authorNickname = "deleted";
                authorReputation = 0;
            }

            questionsModel.Add(new ResearchQuestionModel
                (VoteCount: question.VoteCount,
                AnswersCount: question.Answers.Count,
                ViewsCount: question.Views,
                Title: question.Title,
                Content: question.Content,
                Tags: question.Tags,
                AskingUserPicture: authorAvatar,
                AuthorNickname: authorNickname,
                AuthorReputation: authorReputation,
                AskedAt: question.PostedAt));
        }

        var researchModel = new ResearchModel
        {
            ResearchText = q,
            PageNumber = options.PageNumber,
            OrderBy = options.OrderBy,
            ResultsCount = questionsModel.Count,
            Questions = questionsModel
        };

        return View(researchModel);
    }
}
