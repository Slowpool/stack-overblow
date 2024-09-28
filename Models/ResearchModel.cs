using StackOverblowApp.Models;
using System.Collections.Generic;

namespace stackoverblow.Models;

public class ResearchModel
{
    public IReadOnlyCollection<ResearchQuestionModel> Questions;
    public readonly string ResearchString;
    public ResearchModel(string researchText)
    {
        ResearchString = researchText;
        var list = new List<ResearchQuestionModel>();
        
        FindRelevantQuestionService

    }
}
