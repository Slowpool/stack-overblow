using System.Diagnostics;
using CommonFunctions;
using DataLayer;
using DataLayer.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;
using StackOverblowApp.Models;
using StackOverblowApp.Models.Home;

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

    [HttpGet(template: "/search", Name = "QuestionsSearch")]
    public async Task<IActionResult> Search([FromQuery] string q, [FromQuery] string orderBy=nameof(QuestionsOrderBy.Relevance), [FromQuery] int page = 1)
    {
        if (q.Length == 0)
            return View();

        ViewBag.LayoutModel = new LayoutModel { ResearchText = q };

        if (!Enum.TryParse(orderBy, true, out QuestionsOrderBy orderByOption))
        {
            return View();
        }

        var options = new QuestionResearchOptions(orderByOption, page);
        var service = new QuestionService(_context);
        var questions = service.GetQuestions(q, options);

        var questionsModel = new List<ResearchQuestionModel>();
        string authorAvatar;
        string authorNickname;
        int authorReputation;
        int authorId;
        foreach (var question in questions)
        {
            if (question.AskingUser != null)
            {
                authorAvatar = question.AskingUser.Avatar;
                authorNickname = question.AskingUser.Nickname;
                authorReputation = question.AskingUser.Reputation;
                authorId = question.AskingUser.UserProfileId;
            }
            else
            {
                authorAvatar = "default pic";
                authorNickname = ConstNames.DeletedUser;
                authorReputation = 0;
                authorId = 0;
            }

            questionsModel.Add(new ResearchQuestionModel
                (QuestionId: question.QuestionId,
                VoteCount: question.VoteCount,
                AnswersCount: question.Answers.Count,
                ViewsCount: question.Views,
                Title: question.Title,
                Content: question.Content,
                Tags: question.Tags,
                AskingUserPicture: authorAvatar,
                AuthorNickname: authorNickname,
                AuthorReputation: authorReputation,
                AskedAt: question.PostedAt,
                AuthorId: authorId));
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
