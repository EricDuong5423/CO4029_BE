using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Mappers;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;
using CO4029_BE.Utils;
using Supabase;

namespace AgenticAR.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly Client _supabaseClient;
    private readonly IOtpCodeRepository _otpCodeRepository;

    public AuthService(IUserRepository userRepository, IOtpCodeRepository otpCodeRepository, Client supabaseClient)
    {
        _userRepository = userRepository;
        _supabaseClient = supabaseClient;
        _otpCodeRepository = otpCodeRepository;
    }
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var session = await _supabaseClient.Auth.SignIn(
            email: request.Email,
            password: request.Password
        );
        if (session == null || session.User == null) 
            throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng. Xin vui lòng đăng nhập lại !!!");
        var authUser = session.User;
        
        var userEmail = authUser.Email;
        var user = await _userRepository.GetByEmailAsync(userEmail);

        if (user == null) 
            throw new Exception("Không tìm thấy user");

        return session.ToResponse();
    }
    public async Task<UserReponse> GetMeAsync(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new UnauthorizedAccessException("Token không hợp lệ.");

        // Lấy user Supabase từ access token
        var gotrueUser = await _supabaseClient.Auth.GetUser(accessToken);
        if (gotrueUser == null)
            throw new UnauthorizedAccessException("Token Supabase không hợp lệ hoặc đã hết hạn.");

        var email = gotrueUser.Email;
        if (string.IsNullOrEmpty(email))
            throw new Exception("Không lấy được email từ Supabase user.");

        // Tìm user trong bảng user của bạn
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            throw new Exception("Không tìm thấy user trong database.");

        return user.ToResponse();
    }

    public async Task<bool> SendOtpCode(string accessToken)
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
        var otps = await _otpCodeRepository.GetAllAsync();
        foreach (var otp in otps)
        {
            if (otp.email == user.email && otp.is_verified == false)
            {
                throw new Exception("User đã được gửi otp code");
            }
        }
        string OTP = OTPGenerator.GenerateOTP();
        EmailService.SendOTPMail(user.email, OTP);
        await _otpCodeRepository.CreateAsync(new OtpCode
        {
            otp = OTP,
            email = user.email,
            expires_at = DateTime.UtcNow.AddMinutes(5),
            created_at = DateTime.UtcNow 
        });
        return true;
    }

    public async Task<UserReponse> ChangePasswordAsync(string accessToken, string otpCode, string newPassword, string oldPassword)
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

        bool isOtpValid = await VerifyOTPCode(user,otpCode);
        if (!isOtpValid)
        {
            throw new Exception("OTP không hợp lệ hoặc đã hết hạn.");
        }
        var session = await _supabaseClient.Auth.SignInWithPassword(user.email, oldPassword);
        if (session == null)
            throw new Exception();
        var updatedResult = await _supabaseClient.Auth.Update(new Supabase.Gotrue.UserAttributes
        {
            Password = newPassword
        });
        if (updatedResult == null)
            throw new Exception("Không thể thay đổi mật khẩu cho người dùng.");
        return user.ToResponse();
    }

    private async Task<bool> VerifyOTPCode(User user, string otpCode)
    {
        var otps =  await _otpCodeRepository.GetAllAsync();
        foreach (var otp in otps)
        {
            DateTime expiresAtLocal = otp.expires_at.HasValue 
                ? TimeZoneInfo.ConvertTimeFromUtc(otp.expires_at.Value, TimeZoneInfo.Local) 
                : DateTime.MinValue;
            if (otp.email == user.email && otp.is_verified == false && 
                String.Equals(otp.otp, otpCode) && expiresAtLocal > DateTime.Now)
            {
                otp.is_verified = true;
                await _otpCodeRepository.UpdateAsync(otp.id, otp);
                return true;
            }
        }

        return false;
    }
}