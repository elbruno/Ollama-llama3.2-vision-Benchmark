using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Extensions.AI;

namespace OllamaBenchmark;

[KeepBenchmarkFiles]
[AsciiDocExporter]
[CsvExporter]
[CsvMeasurementsExporter]
[HtmlExporter]
[PlainExporter]
[SimpleJob(RunStrategy.Throughput, iterationCount: 5)] 
public class OllamaImageLabs
{
    private const string promptDescribeImage = @"Analyze the image, return a JSON object with the field 'Description'. 
Include the description of the image in the JSON field 'Description'.
Include the description in English, French and Spanish.
Only provide the JSON result and nothing else. 
Return only the JSON object without any markdown. ";

    private const string promptAnalyzeTrafficCam = @"Analyze the image, return a JSON object with the fields 'Title', 'Traffic' and 'Date'. 
Extract the text from the top left corner of the image and assign the extracted text to the JSON field 'Title'. 
Extract the text from the bottom right corner of the image and assign the extracted text to the JSON field 'Date'. 
Analyze the amount of traffic in the image. Based on the amount of traffic, define a value from 0 to 100, where 0 is no traffic and 100 is heavy traffic. Assign the integer value of the traffic to the JSON field 'Traffic'.
Only provide the JSON result and nothing else. 
Return only the JSON object without any markdown. ";


    [Benchmark(Description = "Ollama-GPU-llama3.2-vision-Sample-images")]
    [BenchmarkCategory("GPU", "Sample Images")]
    public async Task GPUllama32vision_sampleimages_Async()
    {

        await ProcessImagesAsync("http://localhost:11434/", "gpu-sample-images", "llama3.2-vision", promptDescribeImage, @"images\sampleimages");
    }

    [Benchmark(Description = "Ollama-CPU-llama3.2-vision-Sample-images")]
    [BenchmarkCategory("CPU", "Sample Images")]
    public async Task CPUllama32vision_sampleimages_Async()
    {
        await ProcessImagesAsync("http://localhost:11435/", "cpu-sample-images", "llama3.2-vision", promptDescribeImage, @"images\sampleimages");
    }

    [Benchmark(Description = "Ollama-GPU-llama3.2-vision-TrafficCams")]
    [BenchmarkCategory("GPU", "Traffic Cams")]
    public async Task GPUllama32visionAsync()
    {
        await ProcessImagesAsync("http://localhost:11434/", "gpu-traffic-cam", "llama3.2-vision", promptAnalyzeTrafficCam, @"images\trafficcams");
    }

    [Benchmark(Description = "Ollama-CPU-llama3.2-vision-TrafficCams")]
    [BenchmarkCategory("CPU", "Traffic Cams")]
    public async Task CPUllama32visionAsync()
    {
        await ProcessImagesAsync("http://localhost:11435/", "gpu-traffic-cam", "llama3.2-vision", promptAnalyzeTrafficCam, @"images\trafficcams");
    }

    private static async Task ProcessImagesAsync(string ollamaUri, string dockerType, string modelId, string prompt, string filesDirectory)
    {
        // load images and create a chat client
        var images = Directory.GetFiles(filesDirectory);
        var chatGpu = new OllamaChatClient(new Uri(ollamaUri), modelId);

        // process each image with the specified prompt
        foreach (var image in images)
        {
            AIContent aic = new ImageContent(File.ReadAllBytes(image), "image/jpeg");
            var imageChatMessage = new ChatMessage(ChatRole.User, contents: [aic]);
            var messages = new List<ChatMessage> { imageChatMessage, new(ChatRole.System, prompt) };
            var result = await chatGpu.CompleteAsync(messages);
            var content = result.Message.Text!;
            Console.WriteLine($"{dockerType} - {modelId} - {image}: {content}");
        }
    }
}