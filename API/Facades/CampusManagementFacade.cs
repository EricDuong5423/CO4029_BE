using AgenticAR.Application.Services;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CO4029_BE.Facades;

[ApiController]
[Route("buildings")]
public class CampusManagementFacade : Controller
{
    private readonly CampusService campusService;
    private readonly UserService userService;

    public CampusManagementFacade(CampusService campusService, UserService userService)
    {
        this.campusService = campusService;
        this.userService = userService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<BuildingReponse>>> GetBuildings()
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await campusService.GetAllBuildings(token);
            return Ok(new
            {
                success = true,
                message = "Lấy tất cả tòa nhà thành công",
                data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{buildingId}")]
    public async Task<ActionResult<BuildingReponse>> GetBuilding(string buildingId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await campusService.GetBuildingById(buildingId, token);
            return Ok(new
            {
                success = true,
                message = "Lấy tòa nhà thành công",
                data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<BuildingReponse>> CreateBuilding([FromBody] CreateBuildingRequest request)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await campusService.CreateBuilding(request, token);
            return Ok(new
            {
                success = true,
                message = "Tạo tòa nhà thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{buildingId}")]
    public async Task<ActionResult<BuildingReponse>> UpdateBuilding([FromBody] UpdateBuildingRequest request, string buildingId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await campusService.UpdateBuilding(request, token, buildingId);
            return Ok(new
            {
                success = true,
                message = "Thay đổi thông tin tòa nhà thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{buildingId}")]
    public async Task<ActionResult<BuildingReponse>> DeleteBuilding(string buildingId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await campusService.DeleteBuilding(buildingId, token);
            return Ok(new
            {
                success = true,
                message = "Xóa tòa nhà thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get-by-user-id/{userId}")]
    public async Task<ActionResult<BuildingReponse>> GetBuildingByUserId(string userId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await campusService.GetBuildingsByUserId(token, userId);
            return Ok(new
            {
                success = true,
                message = "Lấy tất cả tòa nhà theo id người dùng thành công",
                data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}