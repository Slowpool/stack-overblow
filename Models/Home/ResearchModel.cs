using DataLayer;
using ServiceLayer;
using System.Collections.Generic;

namespace StackOverblowApp.Models.Home;

public class ResearchModel
{
    public IReadOnlyCollection<ResearchQuestionModel> Questions { get; init; }
    public string ResearchText { get; init; }
    public int PageNumber { get; init; }
    public int ResultsCount { get; init; }
    public QuestionsOrderBy OrderBy { get; init; }
}
