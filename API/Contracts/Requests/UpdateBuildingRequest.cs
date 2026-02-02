namespace CO4029_BE.API.Contracts.Requests;

public class UpdateBuildingRequest
{
    public string? Name { get; set; }
    public string? Content { get; set; }
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
}