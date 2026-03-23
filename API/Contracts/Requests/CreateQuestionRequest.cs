namespace CO4029_BE.API.Contracts.Requests;

public class CreateQuestionRequest
{
    public string? Content { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? CategoryId { get; set; }
    
    public string? UserId { get; set; }
}