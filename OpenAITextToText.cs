using System.Net.Http.Headers;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;



//using System;
//using System.IO;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//public class Program
//{
//    private static OpenAITextToText openAITextToText = new OpenAITextToText();
//    static async Task Main(string[] args)
//    {
//        // Assign values to properties
//        string MyApiKey = "<YOUR API KEY>";
//        // Assign values to properties
//        openAITextToText.SetApiKeyAndAuthenticate(MyApiKey);
//        // Now that the properties are set, you can call the method
//        var response = await openAITextToText.SendPrompt("how many states are in the United States of America?");
//        Console.WriteLine(response);
//    }
//}


public class OpenAITextToText
{

    private static string? _apiKey;

    private HttpClient client;

    public OpenAITextToText()
    {
        client = new HttpClient { };
    }
    public void SetApiKeyAndAuthenticate(string apiKey)
    {
        _apiKey = apiKey;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }



    public async Task<string> SendPrompt(string prompt)
    {
        string apiEndpoint = "https://api.openai.com/v1/chat/completions";


        /* Newer models(2023–) https://api.openai.com/v1/chat/completions   
        gpt-4
        gpt-4-0613
        gpt-4-turbo
        gpt-4-32k
        gpt-4-32k-0613
        gpt-3.5-turbo
        gpt-3.5-turbo-1106

        Updated legacy models(2023) https://api.openai.com/v1/completions   
        gpt-3.5-turbo-instruct, 
        babbage-002, 
        davinci-002
        */
        var requestBody = new
        {
            model = "gpt-4-0125-preview",
            messages = new[] { new { role = "user", content = prompt } },
            max_tokens = 4096,
            temperature = 0.7


        };

        var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");


        try
        {
            var response = await client.PostAsync(apiEndpoint, requestContent);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ChatGPTResponse>(responseJson);

                if (responseData != null && responseData.choices != null && responseData.choices.Length > 0 && responseData.choices[0].message != null)
                {
                    return responseData.choices[0].message.content;
                }
                else
                {
                    return "Response data is missing or incomplete.";
                }
            }
            else
            {
                return $"Error in making the request. Status code: {response.StatusCode}. Response: {await response.Content.ReadAsStringAsync()}";
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }

    private class ChatGPTResponse
    {
        public Choice[] choices { get; set; } = new Choice[0];
    }

    public class Choice
    {
        public Message message { get; set; } = new Message();
    }

    public class Message
    {
        public string content { get; set; } = "";
    }

}
