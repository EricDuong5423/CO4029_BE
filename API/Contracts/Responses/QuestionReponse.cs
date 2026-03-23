namespace CO4029_BE.API.Contracts.Responses;

public class QuestionReponse
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CategoryId { get; set; }
    public string? UserId { get; set; }
}