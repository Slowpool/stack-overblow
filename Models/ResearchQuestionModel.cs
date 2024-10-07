using DataLayer.DomainModel;

namespace StackOverblowApp.Models;

public record class ResearchQuestionModel(int VoteCount, int AnswersCount, int ViewsCount, string Title, string Content, ICollection<Tag> Tags, string AskingUserPicture, string AuthorNickname, int AuthorReputation, DateTimeOffset AskedAt, int AuthorId);
