using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Mappers;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using Supabase;

namespace AgenticAR.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly Client _supabaseClient;

    public AuthService(IUserRepository userRepository, Client supabaseClient)
    {
        _userRepository = userRepository;
        _supabaseClient = supabaseClient;
    }
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var session = await _supabaseClient.Auth.SignIn(
            email: request.Email,
            password: request.Password
        );
        if (session == null || session.User == null) throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng. Xin vui lòng đăng nhập lại !!!");
        var authUser = session.User;
        
        var userEmail = authUser.Email;
        var user = await _userRepository.GetByEmailAsync(userEmail);

        if (user == null) throw new Exception("Không tìm thấy user");
        
        var userResponse = user.ToResponse();

        return new LoginResponse
        {
            AccessToken = session.AccessToken,
            RefreshToken = session.RefreshToken,
            ExpiresAt = session.ExpiresAt(),
            User = userResponse
        };
    }
}