using System.Text.Json.Serialization;

namespace CO4029_BE.API.Contracts.Requests;

public class CreateHistoryRequest
{
    public string? Header { get; set; }
    [JsonIgnore]
    public DateTime? Create_date { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(
        DateTime.UtcNow,
        TimeZoneInfo.FindSystemTimeZoneById(
            System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows) 
                ? "SE Asia Standard Time" 
                : "Asia/Ho_Chi_Minh"
        )
    );
}