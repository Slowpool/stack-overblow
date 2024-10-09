using CommonFunctions;
using DataLayer;
using DataLayer.DomainModel.NotMapped;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using StackOverblowApp.Models.Questions;

namespace StackOverblowApp.Controllers;
public class QuestionsController : Controller
{
    private readonly AppDbContext context;
    public QuestionsController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet(template: "/questions/{questionId:int}", Name = "SpecificQuestions")]
    public IActionResult Index(int questionId)
    {
        var service = new QuestionService(context);
        var question = service.GetQuestion(questionId);
        if (question == null)
            return BadRequest($"The question with id {questionId} was not found");

        return View(new QuestionModel(
            // these lines of code suck (I don't like them because there're many of awkwardnesses everywhere (lines which raise some question in my head because i could write it in another way but i didn't because i don't know which way is better and what the pros and cons of each).)
            Title: question.Title,
            AskedAt: question.PostedAt,
            LastModified: question.LastModified ?? question.PostedAt,
            ViewsCount: question.Views,
            VoteCount: question.VoteCount,
            Content: question.Content,
            Tags: question.Tags,
            AuthorId: question.AskingUser != null ? question.AskingUser.UserProfileId : 0,
            AuthorNickname: question.AskingUser != null ? question.AskingUser.Nickname : ConstNames.DeletedUser,
            AuthorReputation: question.AskingUser != null ? question.AskingUser.Reputation : 0,
            AuthorPicture: question.AskingUser != null ? question.AskingUser.Avatar : "picture",
            Comments: question.Comments,
            Answers: [.. question.Answers]
            ));
    }

    [Authorize]
    public IActionResult Ask()
    {
        return View();
    }
}
