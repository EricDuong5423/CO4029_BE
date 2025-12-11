namespace CO4029_BE.API.Contracts.Requests;

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Gender { get; set; }
}