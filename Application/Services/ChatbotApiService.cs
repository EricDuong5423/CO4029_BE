using System.Net.Http.Json;
using System.Text.Json;
using CO4029_BE.API.Contracts.Requests;
using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace AgenticAR.Application.Services;

public record ChatbotHistoryMessage(string role, string content);
public record ChatbotApiRequest(string message, List<ChatbotHistoryMessage> history);
public record ChatbotApiResponse(string type, JsonElement content);

public class ChatbotApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public ChatbotApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(config["ChatbotUrl"]!);
        _apiKey = config["ChatbotApiKey"] ?? string.Empty;
    }

    public async Task<ChatbotApiResponse> AskAsync(string message, IEnumerable<Chatbox> history)
    {
        var historyList = history
            .OrderBy(m => m.contact_time)
            .Select(m => new ChatbotHistoryMessage(
                m.contact_person == "user" ? "user" : "assistant",
                m.content!))
            .ToList();

        var payload = new ChatbotApiRequest(message, historyList);
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/chat")
        {
            Content = JsonContent.Create(payload)
        };
        httpRequest.Headers.Add("X-API-Key", _apiKey);

        var response = await _httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<ChatbotApiResponse>())!;
    }
}