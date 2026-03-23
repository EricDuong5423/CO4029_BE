using AgenticAR.Application.Services;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CO4029_BE.Facades;

[ApiController]
[Route("contacts")]
public class ContactManagementFacade: Controller
{
    private readonly QuestionService  _questionService;

    public ContactManagementFacade(QuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet("categories/")]
    public async Task<ActionResult<IEnumerable<CategoryReponse>>> GetCategories()
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetCategories(token);
            return Ok(new
            {
                success = true,
                message = "Lấy tất cả danh mục thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("categories/{categoryId}")]
    public async Task<ActionResult<CategoryReponse>> GetCategory(string categoryId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetCategory(categoryId, token);
            return Ok(new
            {
                success = true,
                message = "Lấy danh mục thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("categories/")]
    public async Task<ActionResult<CategoryReponse>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.CreateCategory(request, token);
            return Ok(new
            {
                success = true,
                message = "Tạo danh mục thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("categories/{categoryId}")]
    public async Task<ActionResult<bool>> UpdateCategory(string categoryId, [FromBody] CreateCategoryRequest request)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.UpdateCategory(request, categoryId, token);
            return Ok(new
            {
                success = true,
                message = "Cập nhật danh mục thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("categories/{categoryId}")]
    public async Task<ActionResult<bool>> DeleteCategory(string categoryId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.DeleteCategory(categoryId, token);
            return Ok(new
            {
                success = true,
                message = "Xóa danh mục thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("questions/")]
    public async Task<ActionResult<IEnumerable<QuestionReponse>>> GetQuestions()
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetQuestions(token);
            return Ok(new
            {
                success = true,
                message = "Lấy tất cả câu hỏi thành công",
                data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("questions/{questionId}")]
    public async Task<ActionResult<QuestionReponse>> GetQuestion(string questionId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetQuestion(questionId, token);
            return Ok(new { success = true, message = "Lấy câu hỏi thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("questions/categories/{categoryId}")]
    public async Task<ActionResult<IEnumerable<QuestionReponse>>> GetQuestionsByCategory(string categoryId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetQuestionsByCategory(categoryId, token);
            return Ok(new { success = true, message = "Lấy danh sách câu hỏi theo danh mục thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("questions/")]
    public async Task<ActionResult<QuestionReponse>> CreateQuestion([FromBody] CreateQuestionRequest request)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.CreateQuestion(request, token);
            return Ok(new { success = true, message = "Tạo câu hỏi thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("questions/{questionId}")]
    public async Task<ActionResult<bool>> UpdateQuestion(string questionId, [FromBody] CreateQuestionRequest request)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.UpdateQuestion(request, questionId, token);
            return Ok(new { success = true, message = "Cập nhật câu hỏi thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("questions/{questionId}")]
    public async Task<ActionResult<bool>> DeleteQuestion(string questionId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.DeleteQuestion(questionId, token);
            return Ok(new { success = true, message = "Xóa câu hỏi thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("answers/")]
    public async Task<ActionResult<IEnumerable<AnswerReponse>>> GetAnswers()
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetAnswers(token);
            return Ok(new { success = true, message = "Lấy tất cả câu trả lời thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("answers/{answerId}")]
    public async Task<ActionResult<AnswerReponse>> GetAnswer(string answerId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetAnswer(answerId, token);
            return Ok(new { success = true, message = "Lấy câu trả lời thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("users/{userId}/answers")]
    public async Task<ActionResult<IEnumerable<AnswerReponse>>> GetAnswersByUser(string userId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.GetAnswersByUser(userId, token);
            return Ok(new { success = true, message = "Lấy danh sách câu trả lời của người dùng thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("answers/")]
    public async Task<ActionResult<AnswerReponse>> CreateAnswer([FromBody] CreateAnswerRequest request)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.CreateAnswer(request, token);
            return Ok(new { success = true, message = "Tạo câu trả lời thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("answers/{answerId}")]
    public async Task<ActionResult<bool>> UpdateAnswer(string answerId, [FromBody] CreateAnswerRequest request)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.UpdateAnswer(request, answerId, token);
            return Ok(new { success = true, message = "Cập nhật câu trả lời thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("answers/{answerId}")]
    public async Task<ActionResult<bool>> DeleteAnswer(string answerId)
    {
        try
        {
            var token = await AccessToken.GetAccessToken(HttpContext);
            var result = await _questionService.DeleteAnswer(answerId, token);
            return Ok(new { success = true, message = "Xóa câu trả lời thành công", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}