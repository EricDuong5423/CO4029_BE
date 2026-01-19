namespace CO4029_BE.API.Contracts.Requests;

public class ChangePasswordRequest
{
    public string? email { get; set; }
    public string? newPassword { get; set; }
    public string? oldPassword { get; set; }
    public string? otpCode { get; set; }
}