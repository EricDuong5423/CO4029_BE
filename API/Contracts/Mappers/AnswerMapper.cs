using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;

namespace CO4029_BE.API.Contracts.Mappers;

public static class AnswerMapper
{
    public static AnswerReponse ToReponse(this Answer answer) => new AnswerReponse
    {
        Id = answer.ID,
        Content = answer.Content,
        QuestionId = answer.QuestionID,
        UserId = answer.UserID,
        CreateDate = answer.CreateDate,
    };
}