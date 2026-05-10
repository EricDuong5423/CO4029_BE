using AgenticAR.Infrastructure.Repository;
using CO4029_BE.Domain.Entities;
using CO4029_BE.Utils;
using Supabase;

namespace AgenticAR.Application.Services;

public interface ICurrentUserService
{
    Task<User> GetCurrentUser(string accessToken);
}

public class CurrentUserService : ICurrentUserService
{
    private readonly Client _supabaseClient;
    private readonly IUserRepository _userRepository;

    public CurrentUserService(Client supabaseClient, IUserRepository userRepository)
    {
        _supabaseClient = supabaseClient;
        _userRepository = userRepository;
    }

    public Task<User> GetCurrentUser(string accessToken)
        => AccessToken.GetUser(accessToken, _supabaseClient, _userRepository);
}