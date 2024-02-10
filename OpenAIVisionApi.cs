using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;



//using System;
//using System.IO;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;


//public class Program
//{
//    private static OpenAIVisionService openAIVisionApi = new OpenAIVisionService();
//    static async Task Main(string[] args)
//    {
//        // Assign values to properties
//        string MyApiKey = "<YOUR API KEY>";
//        string MyImagePath = @"c:\<YOUR IMAGE PATH HERE>\Image.jpg";
//        // Assign values to properties
//        openAIVisionApi.SetApiKeyAndAuthenticate(MyApiKey);
//        openAIVisionApi.ImagePath = MyImagePath;
//        // Now that the properties are set, you can call the method
//        var description = await openAIVisionApi.DescribeImageAsync("what do you see in this picture");
//        Console.WriteLine("Description:");
//        Console.WriteLine(description);
//    }
//}




public class OpenAIVisionService
{
    private string ApiKey { get; set; }
    public string ImagePath { get; set; }

    public OpenAIVisionService()
    {
        // Default constructor without parameters
    }

    private string _apiKey;

    public void SetApiKeyAndAuthenticate(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key cannot be null or whitespace.", nameof(apiKey));
        ApiKey = apiKey;

    }



    public async Task<string> DescribeImageAsync(string questionToAsk)
    {
        if (string.IsNullOrEmpty(ApiKey)) throw new InvalidOperationException("API key must be provided.");
        if (string.IsNullOrEmpty(ImagePath)) throw new InvalidOperationException("Image path must be provided.");

        try
        {
            var base64Image = EncodeImageToBase64(ImagePath);
            var payload = new
            {
                model = "gpt-4-vision-preview",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new { type = "text", text = questionToAsk },
                            new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } }
                        }
                    }
                },
                max_tokens = 300
            };

            var response = await PostRequestAsync("https://api.openai.com/v1/chat/completions", payload);
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response);
            return jsonResponse.choices[0].message.content.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return "Error processing image description.";
        }
    }

    private string EncodeImageToBase64(string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        return Convert.ToBase64String(imageBytes);
    }

    private async Task<string> PostRequestAsync(string url, object payload)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }
        }
    }
}
