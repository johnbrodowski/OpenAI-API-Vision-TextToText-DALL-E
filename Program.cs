using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class Program
{
    private static OpenAIImageGenerator _imageGenerator = new OpenAIImageGenerator();
    private static OpenAIVisionService openAIVisionApi = new OpenAIVisionService();
    private static OpenAITextToText openAITextToText = new OpenAITextToText();
    static async Task Main(string[] args)
    {
        string MyApiKey = "<YOUR API KEY>";
        string MyImagePath = @"c:\<YOUR IMAGE PATH HERE>\Image.jpg";

        // Assign values to properties
        _imageGenerator.SetApiKeyAndAuthenticate(MyApiKey);

        // Now that the properties are set, you can call the method
        var imageUrl = await _imageGenerator.GenerateImageAsync("A white Siamese cat");
        Console.WriteLine("Generated Image URL:");
        Console.WriteLine(imageUrl);

         
        // Assign values to properties
        openAIVisionApi.SetApiKeyAndAuthenticate(MyApiKey);
        openAIVisionApi.ImagePath = MyImagePath;

        // Now that the properties are set, you can call the method
        var description = await openAIVisionApi.DescribeImageAsync("what do you see in this picture");
        Console.WriteLine("Description:");
        Console.WriteLine(description);

         
        // Assign values to properties
        openAITextToText.SetApiKeyAndAuthenticate(MyApiKey);

        // Now that the properties are set, you can call the method
        var response = await openAITextToText.SendPrompt("how many states are in the United States of America?");
        Console.WriteLine(response);

 
    }

}
