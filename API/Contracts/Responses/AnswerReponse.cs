namespace CO4029_BE.API.Contracts.Responses;

public class AnswerReponse
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? QuestionId { get; set; }
    public string? UserId { get; set; }
}