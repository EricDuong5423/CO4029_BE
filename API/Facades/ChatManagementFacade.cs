using AgenticAR.Application.Services;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CO4029_BE.Facades;

[ApiController]
[Route("chat")]
public class ChatManagementFacade : Controller
{
    private readonly HistoryService _historyService;

    public ChatManagementFacade(HistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpPost("create-history")]
    public async Task<ActionResult<HistoryReponse>> CreateHistory([FromBody]CreateHistoryRequest createHistoryRequest)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result =  await _historyService.CreateHistory(createHistoryRequest, token);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpGet("get-all-history")]
    public async Task<ActionResult<IEnumerable<HistoryReponse>>> GetAllHistoryByUserId()
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _historyService.GetHistoryByUserId(token);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}