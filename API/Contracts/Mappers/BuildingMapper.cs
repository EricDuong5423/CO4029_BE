using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;

namespace CO4029_BE.API.Contracts.Mappers;

public static class BuildingMapper
{
    public static BuildingReponse ToReponse(this Building building) => new BuildingReponse
    {
        Id = building.ID,
        Name = building.Name,
        Content = building.Content,
        Latitude = building.Latitude,
        Longitude = building.Longitude,
    };
}