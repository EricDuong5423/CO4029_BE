namespace CO4029_BE.API.Contracts.Requests;

public class CreateAnswerRequest
{
    public string? Content { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? QuestionId { get; set; }
    public string? UserId { get; set; }
}