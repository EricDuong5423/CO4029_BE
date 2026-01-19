namespace CO4029_BE.API.Contracts.Requests;

public class CreateHistoryRequest
{
    public string? Header { get; set; }
    public DateTime? Create_date { get; set; } =  DateTime.Now;
}