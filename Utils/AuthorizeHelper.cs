using CO4029_BE.Domain.Entities;
using Supabase;

namespace CO4029_BE.Utils;

public static class AuthorizeHelper
{
    public static bool AuthorizeForEmployee(User user)
    {
        return user.role == "Employee";
    }
}