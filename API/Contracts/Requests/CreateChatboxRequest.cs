using System.Text.Json.Serialization;
using CO4029_BE.Utils;

namespace CO4029_BE.API.Contracts.Requests;

public class CreateChatboxRequest
{
    public string? Content { get; set; }
    [JsonIgnore]
    public DateTime? Contact_time { get; set; } = DateTimeHelper.GetVnNow();
    public string? Contact_person { get; set; }
    public string? History_id { get; set; }
}