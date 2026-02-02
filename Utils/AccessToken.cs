using AgenticAR.Infrastructure.Repository;
using CO4029_BE.Domain.Entities;
using Supabase;

namespace CO4029_BE.Utils;

public static class AccessToken
{
    public static async Task<String> GetAccessToken(HttpContext context)
    {
        // 1. Lấy dòng header có tên "Authorization"
        string authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        // 2. Kiểm tra xem có header không và có bắt đầu bằng "Bearer " không
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            // Debug: In ra xem nó nhận được gì (xóa khi chạy thật)
            Console.WriteLine($"[AuthCheck] Header nhận được: '{authorizationHeader}'"); 
            
            throw new UnauthorizedAccessException("Thiếu Access token trong header hoặc sai định dạng (Bearer ...)");
        }

        // 3. Cắt bỏ chữ "Bearer " (7 ký tự) để lấy phần token phía sau
        return authorizationHeader.Substring("Bearer ".Length).Trim();
    }
    
    public static async Task<User> GetUser(string accessToken, Client _supabaseClient, IUserRepository _userRepository)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new UnauthorizedAccessException("Token không hợp lệ.");
        var gotrueUser = await _supabaseClient.Auth.GetUser(accessToken);
        if (gotrueUser == null)
            throw new UnauthorizedAccessException("Token Supabase không hợp lệ hoặc đã hết hạn.");
        var email = gotrueUser.Email;
        if (string.IsNullOrEmpty(email))
            throw new Exception("Không lấy được email từ Supabase user.");
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            throw new Exception("Không tìm thấy user trong database.");
        return user;
    }
}