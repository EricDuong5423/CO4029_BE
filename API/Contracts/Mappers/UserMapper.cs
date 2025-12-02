using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;

namespace CO4029_BE.API.Contracts.Mappers;

public static class UserMapper
{
    public static UserReponse ToResponse(this User user)
        => new UserReponse 
        {
            Id = user.id,
            Email = user.email,
            Name = user.name,
            Phone = user.phone,
            Birthday = user.birthday
        }
;
}