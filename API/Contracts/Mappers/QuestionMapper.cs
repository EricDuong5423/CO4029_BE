using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;

namespace CO4029_BE.API.Contracts.Mappers;

public static class QuestionMapper
{
    public static QuestionReponse ToReponse(this Question question) => new QuestionReponse
    {
        Id = question.ID,
        Name = question.Name,
        Content = question.Content,
        CreateDate = question.CreateDate,
        Email = question.Email,
        CategoryId = question.CategoryID,
        UserId = question.UserID,
    };
}