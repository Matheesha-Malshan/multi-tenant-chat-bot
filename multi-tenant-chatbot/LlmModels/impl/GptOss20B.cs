using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.LlmModels.impl;

public class GptOss20B:IModels
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey = "YOUR_API_KEY";

    public GptOss20B(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<string> CreateChat(string userQuery,LlmConfigurationsDto configDto)
    {
        
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);

        var requestBody = new
        {
            messages = new[]
            {
                new { role = "user", content = "hei dear" }
            },
            model = "openai/gpt-oss-120b",
            temperature = 1,
            max_completion_tokens = 8192,
            top_p = 1,
            stream = true,
            reasoning_effort = "medium",
            stop = (string?)null
        };

        var json = JsonSerializer.Serialize(requestBody);

        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await client.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            CancellationToken.None
        );

        response.EnsureSuccessStatusCode();

        // STREAMING READER
        var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        var fullText = new StringBuilder();

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            // Each SSE line begins with "data:"
            if (line.StartsWith("data:"))
            {
                var jsonChunk = line.Substring(5).Trim();

                if (jsonChunk == "[DONE]") 
                    break;

                try
                {
                    using JsonDocument doc = JsonDocument.Parse(jsonChunk);
                    var delta = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("delta");

                    if (delta.TryGetProperty("content", out var content))
                    {
                        var text = content.GetString();
                        fullText.Append(text);

                        // OPTIONAL: print live tokens to console 
                        Console.Write(text);
                    }
                }
                catch
                {
                    // bad chunk - skip
                }
            }
        }

        return fullText.ToString();
    }
    
    
    public bool CheckModelType(LlmModelsDto llmModelsDto)
    {
        return llmModelsDto == LlmModelsDto.GptOss20B;
    }

    
}