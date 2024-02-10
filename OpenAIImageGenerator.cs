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
//    private static OpenAIImageGenerator imageGenerator = new OpenAIImageGenerator();

//    static async Task Main(string[] args)
//    {
//        try
//        {
//            imageGenerator.ApiKey = "<YOUR API KEY>";
//            var imageUrl = await imageGenerator.GenerateImageAsync("a white siamese cat with long rabbit ears.");
//            Console.WriteLine("Generated Image URL:");
//            Console.WriteLine(imageUrl);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"An error occurred: {ex.Message}");
//        }
         
//    }
//}


public class OpenAIVisionService
{
    // Use properties instead of public fields
    public string ApiKey { get; set; }
    public string ImagePath { get; set; }
 
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
