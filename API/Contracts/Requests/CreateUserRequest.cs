namespace CO4029_BE.API.Contracts.Requests;

public class CreateUserRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public string? Name { get; set; }
    public string? Phone { get; set; }
    public DateTime? Birthday { get; set; }
    public string? CCCD { get; set; }
    public string? Role { get; set; }
    public string? Gender { get; set; }
}
