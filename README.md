
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

Each class is responsible for handling specific types of requests to the OpenAI API, abstracting the complexity of HTTP requests and response parsing.

### Authentication

Before making any API requests, it's essential to authenticate with the OpenAI API using a valid API key. This is facilitated through the `SetApiKeyAndAuthenticate` method present in each service class.

```csharp
string MyApiKey = "<YOUR API KEY>";
_imageGenerator.SetApiKeyAndAuthenticate(MyApiKey);
openAIVisionApi.SetApiKeyAndAuthenticate(MyApiKey);
openAITextToText.SetApiKeyAndAuthenticate(MyApiKey);
```

### Performing Operations

- **Image Generation**: Pass a descriptive text prompt to generate an image URL.
- **Vision**: Provide an image path, and the service will describe what it sees.
- **Text-to-Text**: Send a text prompt to receive a response based on the model's understanding.

### Usage Example

```csharp
static async Task Main(string[] args)
{
    // Set API Key for each service
    string MyApiKey = "<YOUR API KEY>";
    
    // Image Generation
    var imageUrl = await _imageGenerator.GenerateImageAsync("A white Siamese cat");
    Console.WriteLine("Generated Image URL: " + imageUrl);
    
    // Vision Analysis
    string MyImagePath = @"c:\<YOUR IMAGE PATH HERE>\Image.jpg";
    var description = await openAIVisionApi.DescribeImageAsync("what do you see in this picture");
    Console.WriteLine("Description: " + description);
    
    // Text-to-Text Processing
    var response = await openAITextToText.SendPrompt("how many states are in the United States of America?");
    Console.WriteLine(response);
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

### Running the Application

Execute the program from your IDE or the command line. Interact with the console application according to the prompts to utilize different OpenAI services.

## Contribution

Contributions are welcome! Please feel free to submit pull requests or create issues for bugs and feature requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

This README provides a structured overview of integrating OpenAI services into a C# application, from setup and authentication to executing specific AI-driven tasks. It's designed to be both informative and accessible, catering to developers of varying skill levels interested in exploring AI capabilities within their .NET applications.
