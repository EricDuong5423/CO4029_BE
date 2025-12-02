namespace CO4029_BE.API.Contracts.Requests;

public class LoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}