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
            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
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
    public async Task<ActionResult<UserReponse>> Login([FromBody] LoginRequest request)
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

}