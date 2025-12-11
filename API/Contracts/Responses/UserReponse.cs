namespace CO4029_BE.API.Contracts.Responses;

public class UserReponse
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Role { get; set; }
    public string? Gender { get; set; }
}