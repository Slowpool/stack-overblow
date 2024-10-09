using DataLayer.DomainModel;

namespace StackOverblowApp.Models.Questions;

public record class QuestionModel(string Title, DateTimeOffset AskedAt, DateTimeOffset LastModified, int ViewsCount, int VoteCount, string Content, IList<Tag> Tags, int AuthorId, string AuthorNickname, int AuthorReputation, string AuthorPicture, IList<Comment> Comments, IList<Answer> Answers);
