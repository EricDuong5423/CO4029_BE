using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Mappers;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;
using Supabase;

namespace AgenticAR.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly Client _supabaseClient;

    public UserService(IUserRepository userRepository, Client supabaseClient)
    {
        _userRepository = userRepository;
        _supabaseClient = supabaseClient;
    }

    public async Task<UserReponse> RegisterUserAsync(CreateUserRequest request)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing != null)
            throw new InvalidOperationException("Email đã được sử dụng.");

        var metadata = new Dictionary<string, object?>
        {
            ["name"] = request.Name,
            ["phone"] = request.Phone,
            ["birthday"] = request.Birthday?.ToString("yyyy-MM-dd")
        };

        var signUpResult = await _supabaseClient.Auth.SignUp(
            request.Email,
            request.Password,
            new Supabase.Gotrue.SignUpOptions
            {
                Data = metadata
            }
        );


        var authUser = signUpResult?.User;
        if (authUser == null)
            throw new Exception("Không thể tạo tài khoản Supabase Auth.");
        
        
        var user = new User
        {
            id = Guid.Parse(authUser.Id),
            email = request.Email,
            name = request.Name,
            phone = request.Phone,
            birthday = request.Birthday
        };

        var created = await _userRepository.CreateAsync(user);
        return created.ToResponse();
    }
}