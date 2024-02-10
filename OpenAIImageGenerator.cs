using System; 
using System.Net.Http.Headers;
using System.Text; 
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
//    private static OpenAIImageGenerator openAIImageGenerator = new OpenAIImageGenerator();
//    static async Task Main(string[] args)
//    {
//        // Assign values to properties
//        string MyApiKey = "<YOUR API KEY>";
//        // Assign values to properties
//        openAIImageGenerator.SetApiKeyAndAuthenticate(MyApiKey);
//        // Now that the properties are set, you can call the method
//        var imageUrl = await openAIImageGenerator.GenerateImageAsync("A white Siamese cat");
//        Console.WriteLine("Generated Image URL:");
//        Console.WriteLine(imageUrl);
//    }
//}



public class OpenAIImageGenerator
{
    private string _apiKey;
 
    public void SetApiKeyAndAuthenticate(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key cannot be null or whitespace.", nameof(apiKey));
        _apiKey = apiKey;
 
    }
 
    public async Task<string> GenerateImageAsync(string prompt, string size = "1024x1024", string quality = "standard", int n = 1)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            throw new InvalidOperationException("API key must be set before calling GenerateImageAsync.");

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                model = "dall-e-3",
                prompt = prompt,
                size = size,
                quality = quality,
                n = n
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/images/generations", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API request failed with status code {response.StatusCode}, message: {await response.Content.ReadAsStringAsync()}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            string imageUrl = responseObject.data[0].url;
            return imageUrl;
        }
    }
}
