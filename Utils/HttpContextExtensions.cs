namespace CO4029_BE.Utils;

public static class HttpContextExtensions
{
    public static string? GetAccessToken(this HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(authHeader)) return null;

        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return authHeader.Substring("Bearer ".Length).Trim();

        return authHeader.Trim();
    }
}