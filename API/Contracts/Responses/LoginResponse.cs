namespace CO4029_BE.API.Contracts.Responses;

public class LoginResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime? ExpiresAt { get; set; } = default!;
    
    public UserReponse User { get; set; } = default!;
}