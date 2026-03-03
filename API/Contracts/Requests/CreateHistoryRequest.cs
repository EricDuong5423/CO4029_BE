using System.Text.Json.Serialization;
using CO4029_BE.Utils;

namespace CO4029_BE.API.Contracts.Requests;

public class CreateHistoryRequest
{
    public string? Header { get; set; }
    [JsonIgnore]
    public DateTime? Create_date { get; set; } = DateTimeHelper.GetVnNow();
}