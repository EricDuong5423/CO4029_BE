using AgenticAR.Application.Services;
using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supabase;

namespace CO4029_BE.Facades;

[ApiController]
[Route("users")]
public class UserManagementFacade : Controller
{
    private readonly UserService userService;
    private readonly AuthService authService;

    public UserManagementFacade(UserService userService, AuthService authService)
    {
        this.userService = userService;
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserReponse>> Register([FromBody] CreateUserRequest request)
    {
        try
        {
            var user = await userService.RegisterUserAsync(request);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await authService.LoginAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
    
    [HttpGet("me")]
    public async Task<ActionResult<UserReponse>> GetMe()
    {
        try
        {
            var token = HttpContext.GetAccessToken();
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized(new { message = "Missing or invalid Authorization header" });

            var userResponse = await authService.GetMeAsync(token);
            return Ok(userResponse);

        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("update")]
    public async Task<ActionResult<UserReponse>> Update([FromBody] UpdateUserRequest request)
    {
        try
        {
            var token = HttpContext.GetAccessToken();
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized(new { message = "Missing or invalid Authorization header" });
            
            var result = await userService.UpdateUserAsync(request, token);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("request-password-change")]
    public async Task<ActionResult> RequestPasswordChange()
    {
        try
        {
            var token = HttpContext.GetAccessToken();
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized(new { message = "Missing or invalid Authorization header" });
            bool result = await authService.SendOtpCode(token);
            return Ok(new {message = "Gửi OTP thành công"});
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<UserReponse>> ChangePassword([FromBody]  ChangePasswordRequest request)
    {
        try
        {
            var token = HttpContext.GetAccessToken();
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized(new { message = "Missing or invalid Authorization header" });
            var result = await authService.ChangePasswordAsync(token,request.otpCode, request.newPassword, request.oldPassword);
            return Ok(new {message = "Đổi mật khẩu thành công", result = result});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message });
        }
    }
}