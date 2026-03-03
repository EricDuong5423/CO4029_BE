public static class DateTimeHelper
{
    public static DateTime GetVnNow()
    {
        DateTime utcNow = DateTime.UtcNow;
        
        TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
            System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows) 
                ? "SE Asia Standard Time" 
                : "Asia/Ho_Chi_Minh"
        );

        return TimeZoneInfo.ConvertTimeFromUtc(utcNow, vnTimeZone);
    }
}