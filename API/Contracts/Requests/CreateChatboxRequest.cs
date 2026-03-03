using System.Text.Json.Serialization;

namespace CO4029_BE.API.Contracts.Requests;

public class CreateChatboxRequest
{
    public string? Content { get; set; }
    [JsonIgnore]
    public DateTime? Contact_time { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById(
            System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows) 
                ? "SE Asia Standard Time" 
            : "Asia/Ho_Chi_Minh"
        )
    );
    public string? Contact_person { get; set; }
    public string? History_id { get; set; }
}