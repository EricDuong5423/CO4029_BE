using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using Supabase.Gotrue;
using User = CO4029_BE.Domain.Entities.User;

namespace CO4029_BE.API.Contracts.Mappers;

public static class UserMapper
{
    public static UserReponse ToResponse(this User user)
        => new UserReponse
        {
            Email = user.email,
            Name = user.name,
            Phone = user.phone,
            Birthday = user.birthday,
            Role = user.role,
            Gender = user.gender
        };
    public static LoginResponse ToResponse(this Session session)
        => new LoginResponse
        {
            AccessToken = session.AccessToken,
            RefreshToken = session.RefreshToken,
            ExpiresAt = session.ExpiresAt(),
        }
;
}