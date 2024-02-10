 


# OpenAI Services Integration in C#

Some classes for basic use of OpenAI API for Vision, TextToText Generation and DALL-E TextToImage Generation.

This repository contains a comprehensive C# application designed to interact with OpenAI's API for various service, including image generation, vision capabilities, and text-to-text transformations. Utilizing the robust capabilities of OpenAI, this application demonstrates how to seamlessly integrate AI-driven features into .NET applications.

## Features

- **Image Generation**: Leverages OpenAI's DALL-E model to generate images based on textual descriptions.
- **Vision Service**: Utilizes OpenAI's vision models to analyze images and provide descriptive insights.
- **Text-to-Text Processing**: Employs OpenAI's GPT models to answer queries or perform text transformations based on user inputs.

## How It Works

### Setup

The application is structured around three main service classes:

- `OpenAIImageGenerator`
- `OpenAIVisionService`
- `OpenAITextToText`
- `OpenAITextToTextStream`
Each class is responsible for handling specific types of requests to the OpenAI API, abstracting the complexity of HTTP requests and response parsing.

### Authentication

Before making any API requests, it's essential to authenticate with the OpenAI API using a valid API key. This is facilitated through the `SetApiKeyAndAuthenticate` method present in each service class.

```csharp
string MyApiKey = "<YOUR API KEY>";
openAIImageGenerator.SetApiKeyAndAuthenticate(MyApiKey);
openAIVisionApi.SetApiKeyAndAuthenticate(MyApiKey);
openAITextToText.SetApiKeyAndAuthenticate(MyApiKey);
openAITextToTextStream.SetApiKeyAndAuthenticate(MyApiKey);
 
```

### Performing Operations

- **Image Generation**: Pass a descriptive text prompt to generate an image URL.
- **Vision**: Provide an image path, and the service will describe what it sees.
- **Text-to-Text**: Send a text prompt to receive a response based on the model's understanding.

### Usage Example

```csharp

private static OpenAIImageGenerator openAIImageGenerator = new OpenAIImageGenerator();
private static OpenAIVisionApi openAIVisionApi = new OpenAIVisionApi();
private static OpenAITextToText openAITextToText = new OpenAITextToText();
private static OpenAITextToTextStream openAITextToTextStream = new OpenAITextToTextStream();

static async Task Main(string[] args)
{
        // Set API Key for each service
        string MyApiKey = "<YOUR API KEY>";
        string MyImagePath = @"c:\<YOUR IMAGE PATH HERE>\Image.jpg";
    
        // Assign values to properties
        openAIImageGenerator.SetApiKeyAndAuthenticate(MyApiKey);
        // Now that the properties are set, you can call the method
        var imageUrl = await openAIImageGenerator.GenerateImageAsync("A white Siamese cat");
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

        // Assign values to properties
        openAITextToTextStream.SetApiKeyAndAuthenticate(MyApiKey);
        //Now that the properties are set, you can call the method
        await openAITextToTextStream.StreamChatCompletionAsync("list all animals in the same family as the cow");
}
```

## Getting Started

### Prerequisites

- .NET SDK compatible with C# 9.0 or later.
- An active OpenAI API key.
- Newtonsoft.Json package for JSON serialization/deserialization.
- HttpClient for making HTTP requests.

### Installation

1. Clone the repository or download the source code.
2. Ensure the `Newtonsoft.Json` package is installed:
   ```
   Install-Package Newtonsoft.Json
   ```
3. Replace `<YOUR API KEY>` with your actual OpenAI API key in the `Main` method.

 

###OpenAIImageGenerator Usage
 
#### 🚀 Function: `GenerateImageAsync`
- **Summary**: Generates an image based on a text description using OpenAI's image generation API.
- **Details**: Sets the API key, then requests the generation of an image that matches the provided text description.
- **Parameters**:
  - `MyApiKey` (string): Your OpenAI API key.
  - `"A white Siamese cat"` (string): Text description of the image to generate.
- **Returns**: The URL of the generated image.
- **Example**: 
  ```csharp
  var imageUrl = await openAIImageGenerator.GenerateImageAsync("A white Siamese cat");
  Console.WriteLine("Generated Image URL:");
  Console.WriteLine(imageUrl);
 

### OpenAIVisionApi Usage

 
#### 🚀 Function: `DescribeImageAsync`
- **Summary**: Describes the content of an image using OpenAI's vision API.
- **Details**: After setting the API key and specifying the image path, this method analyzes the image and returns a descriptive text of its content.
- **Parameters**:
  - `MyApiKey` (string): Your OpenAI API key.
  - `MyImagePath` (string): The file path to the image you want described.
- **Returns**: A descriptive text of the image content.
- **Example**: 
  ```csharp
  var description = await openAIVisionApi.DescribeImageAsync("what do you see in this picture");
  Console.WriteLine("Description:");
  Console.WriteLine(description);
 

### OpenAITextToText Usage

 
#### 🚀 Function: `SendPrompt`
- **Summary**: Sends a text prompt to OpenAI's text-to-text API and returns the response.
- **Details**: Sets the API key, then sends a specified prompt to the API. Useful for querying information or generating text based on a prompt.
- **Parameters**:
  - `MyApiKey` (string): Your OpenAI API key.
  - `"how many states are in the United States of America?"` (string): The prompt to send to the API.
- **Returns**: The API's text response to the prompt.
- **Example**: 
  ```csharp
  var response = await openAITextToText.SendPrompt("how many states are in the United States of America?");
  Console.WriteLine(response);
 

### OpenAITextToTextStream Usage

 
#### 🚀 Function: `StreamChatCompletionAsync`
- **Summary**: Streams responses from OpenAI's Chat API based on a given prompt.
- **Details**: After setting the API key, this method initiates a streaming session with the Chat API to continuously receive and process responses based on the input prompt.
- **Parameters**:
  - `MyApiKey` (string): Your OpenAI API key.
  - `"list all animals in the same family as the cow"` (string): The prompt to stream chat completions for.
- **Returns**: N/A (The method streams data and outputs to the console).
- **Example**: 
  ```csharp
  await openAITextToTextStream.StreamChatCompletionAsync("list all animals in the same family as the cow");



### Running the Application

Execute the program from your IDE or the command line. Interact with the console application according to the prompts to utilize different OpenAI services.

## Contribution

Contributions are welcome! Please feel free to submit pull requests or create issues for bugs and feature requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

This README provides a structured overview of integrating OpenAI services into a C# application, from setup and authentication to executing specific AI-driven tasks. It's designed to be both informative and accessible, catering to developers of varying skill levels interested in exploring AI capabilities within their .NET applications.
