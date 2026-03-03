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
    private readonly ChatboxService _chatboxService;

    public ChatManagementFacade(HistoryService historyService
                              , ChatboxService chatboxService)
    {
        _historyService = historyService;
        _chatboxService = chatboxService;
    }

    [HttpPost("histories/")]
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
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("histories/")]
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
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("chatboxes/")]
    public async Task<ActionResult<ChatboxReponse>> CreateChatbox([FromBody] CreateChatboxRequest createChatboxRequest)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _chatboxService.CreateChatbox(token, createChatboxRequest);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpGet("chatboxes/")]
    public async Task<ActionResult<IEnumerable<ChatboxReponse>>> GetAllChatboxes()
    {
        try
        {
            var  token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _chatboxService.GetAllChatboxes(token);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpGet("chatboxes/{chatboxId}")]
    public async Task<ActionResult<ChatboxReponse>> GetChatbox([FromRoute] string chatboxId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _chatboxService.GetChatboxById(token, chatboxId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpGet("chatboxes/history/{historyId}")]
    public async Task<ActionResult<IEnumerable<ChatboxReponse>>> GetChatboxesByHistory([FromRoute] string historyId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _chatboxService.GetAllChatboxesByHistory(token, historyId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpDelete("chatboxes/{chatboxId}")]
    public async Task<ActionResult> DeleteChatbox([FromRoute] string chatboxId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            await _chatboxService.DeleteChatbox(token, chatboxId);
            return Ok("Đã xóa thành công chatbox");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}