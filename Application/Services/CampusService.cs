using AgenticAR.Infrastructure.Repository;
using CO4029_BE.API.Contracts.Mappers;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;
using CO4029_BE.Utils;
using Supabase;

namespace AgenticAR.Application.Services;

public class CampusService
{
    private readonly IBuildingRepository buildingRepository;
    private readonly IUserRepository userRepository;
    private readonly Client supabaseClient;
    private readonly IConfiguration configuration;

    public CampusService(IBuildingRepository buildingRepository,
                         IUserRepository userRepository,
                         IConfiguration configuration,
                         Client supabaseClient)
    {
        this.buildingRepository = buildingRepository;
        this.userRepository = userRepository;
        this.supabaseClient = supabaseClient;
        this.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public async Task<IEnumerable<BuildingReponse>> GetAllBuildings(string accessToken)
    {
        var user = await AccessToken.GetUser(accessToken, supabaseClient, userRepository);

        if (!AuthorizeHelper.AuthorizeForEmployee(user))
        {
            throw new UnauthorizedAccessException("Bạn không có quyền để sử dụng API này");
        }

        var buildings = await buildingRepository.GetAllAsync();
        return buildings.Select(building => building.ToReponse());
    }

    public async Task<BuildingReponse> GetBuildingById(string buildingId, string accessToken)
    {
        var user = await AccessToken.GetUser(accessToken, supabaseClient, userRepository);

        if (!AuthorizeHelper.AuthorizeForEmployee(user))
        {
            throw new UnauthorizedAccessException("Bạn không có quyền để sử dụng API này");
        }

        var building = await buildingRepository.GetByIdAsync(buildingId);
        return building.ToReponse();
    }

    public async Task<BuildingReponse> CreateBuilding(CreateBuildingRequest request, string accessToken)
    {
        var user = await AccessToken.GetUser(accessToken, supabaseClient, userRepository);

        if (!AuthorizeHelper.AuthorizeForEmployee(user))
        {
            throw new UnauthorizedAccessException("Bạn không có quyền để sử dụng API này");
        }

        var building = new Building
        {
            Name = request.Name,
            Content = request.Content,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            UserId = user.id
        };
        
        var reponse = await buildingRepository.CreateAsync(building);
        return reponse.ToReponse();
    }

    public async Task<IEnumerable<BuildingReponse>> GetBuildingsByUserId(string accessToken, string UserID)
    {
        var user = await AccessToken.GetUser(accessToken, supabaseClient, userRepository);

        if (!AuthorizeHelper.AuthorizeForEmployee(user))
        {
            throw new UnauthorizedAccessException("Bạn không có quyền để sử dụng API này");
        }
        var buildings = await buildingRepository.GetBuildingsByUserId(user.id);
        
        return buildings.Select(building => building.ToReponse());
    }

    public async Task<BuildingReponse> UpdateBuilding(UpdateBuildingRequest request, string accessToken, string buildingId)
    {
        var user = await AccessToken.GetUser(accessToken, supabaseClient, userRepository);
        if (!AuthorizeHelper.AuthorizeForEmployee(user))
        {
            throw new UnauthorizedAccessException("Bạn không có quyền để sử dụng API này");
        }
        
        var building = await buildingRepository.GetByIdAsync(buildingId);
        if (building == null) throw new Exception("Không tồn tại building");
        
        building.Name = request.Name ?? building.Name;
        building.Content = request.Content ?? building.Content;
        building.Latitude = request.Latitude ?? building.Latitude;
        building.Longitude = request.Longitude ?? building.Longitude;
        
        await buildingRepository.UpdateAsync(buildingId, building);

        var reponse = await buildingRepository.GetByIdAsync(buildingId);
        return reponse.ToReponse();
    }

    public async Task<BuildingReponse> DeleteBuilding(string buildingId, string accessToken)
    {
        var user = await AccessToken.GetUser(accessToken, supabaseClient, userRepository);
        if (!AuthorizeHelper.AuthorizeForEmployee(user))
        {
            throw new UnauthorizedAccessException("Bạn không có quyền để sử dụng API này");
        }
        
        var building = await buildingRepository.GetByIdAsync(buildingId);
        if(building == null) throw new Exception("Không tồn tại building");
        
        if(building.UserId != user.id) throw new UnauthorizedAccessException("Bạn không có quyền xóa building của người khác");
        
        await buildingRepository.DeleteAsync(buildingId);
        
        return building.ToReponse();
    }
}