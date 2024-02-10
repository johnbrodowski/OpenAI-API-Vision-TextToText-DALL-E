using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class OpenAIChatStreamService
{
    private readonly HttpClient _httpClient;
    private string _apiKey;
 
    public OpenAIChatStreamService()
    {
        _httpClient = new HttpClient { };
    }
    public void SetApiKeyAndAuthenticate(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task StreamChatCompletionAsync(string prompt)
    {
        var payload = new
        {
            model = "gpt-4",
            messages = new[] { new { role = "user", content = prompt } },
            stream = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Request failed: {response.StatusCode}");
            return;
        }

        var stream = await response.Content.ReadAsStreamAsync();
        using (var reader = new StreamReader(stream))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                // Check for the special end-of-stream marker and empty lines
                if (string.IsNullOrWhiteSpace(line) || line.Trim().Equals("data: [DONE]", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Skip to the next iteration for empty lines or end-of-stream marker
                }

                // Remove "data:" prefix if present
                if (line.StartsWith("data:"))
                {
                    line = line.Substring(5);
                }

                try
                {
                    var token = JToken.Parse(line); // Parse the cleaned line as JSON

                    // Extract and print only the 'content' part of the delta within choices
                    var contentX = token.SelectToken("choices[0].delta.content")?.ToString();
                    if (!string.IsNullOrEmpty(contentX))
                    {
                        Console.WriteLine(contentX); // Print the content directly
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON parsing error: {ex.Message} for line: {line}");
                }
            }
        }

    }

}
