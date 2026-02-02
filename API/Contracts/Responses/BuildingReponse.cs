namespace CO4029_BE.API.Contracts.Responses;

public class BuildingReponse
{
    public String? Id { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
}