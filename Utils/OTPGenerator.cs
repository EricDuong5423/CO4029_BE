namespace CO4029_BE.Utils;

public class OTPGenerator
{
    public static string GenerateOTP(int length = 4)
    {
        Random random = new Random();
        string otp = "";
        for (int i = 0; i < length; i++)
        {
            otp += random.Next(0, 9).ToString();
        }
        return otp;
    }
}