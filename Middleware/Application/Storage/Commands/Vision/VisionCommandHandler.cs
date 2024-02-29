using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.Core;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels;

namespace Application.Storage.Commands.Vision
{
    internal class VisionCommandHandler(BlobServiceClient blobServiceClient, IOpenAIService openai) : IRequestHandler<VisionCommand>
    {
        private const string Endpoint = "https://analisedocs.cognitiveservices.azure.com/"; //"https://searchforapp.search.windows.net";
        private const string SubscriptionKey = "8f4eebdcc5044e12aef4319a6723965b";
        private readonly IOpenAIService _openai = openai;


        public async Task<Unit> Handle(VisionCommand request, CancellationToken cancellationToken)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(request.ContainerName);

            var credential = new AzureKeyCredential(SubscriptionKey);

            var analyzeResults = new List<AnalyzeResult>();
            var client = new DocumentAnalysisClient(new Uri(Endpoint), credential);//prebuilt-read prebuilt-document

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                var doc = await ExtractTextFromBlob(client, blobClient.Uri);
                analyzeResults.Add(doc);

            }

            var analyzeUserQuery = await AnalyzeUserQuery(request.UserQuery);

            var queryKeywords = new List<string> { "citizens" }; //analyzeUserQuery!.Split(',');

            var relevantParagraphs = new List<string>();


            foreach (var result in analyzeResults)
            {
                foreach (var paragraph in result.Paragraphs)
                {
                    var paragraphText = string.Join(" ", paragraph.Content);
                    if (queryKeywords.Any(keyword => paragraphText.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        relevantParagraphs.Add(paragraphText);
                    }

                }
            }

            var messages = new List<ChatMessage>
            {
                //ChatMessage.FromSystem("получения ключевых слов запроса."),
                ChatMessage.FromUser($"Найденные сведения: {string.Join("\n", relevantParagraphs)}")
            };

            var completionRequest = new ChatCompletionCreateRequest
            {
                Messages = messages,
                Model = "gpt-4",
                MaxTokens = 100,
                Temperature = (float?)0.7,
                TopP = (float?)1.0,
                FrequencyPenalty = (float?)0.5,
                PresencePenalty = (float?)0.5,

            };

            var completionResult = await openai.ChatCompletion.CreateCompletion(completionRequest, cancellationToken: cancellationToken);

            var openAiResponse = completionResult.Choices.First().Message.Content;


            return Unit.Value;
        }

        static async Task<AnalyzeResult> ExtractTextFromBlob(DocumentAnalysisClient client, Uri blobUri )
        {

            //var operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-document", blobUri);

            var operation = await client.AnalyzeDocumentFromUriAsync(
                WaitUntil.Completed, 
                "prebuilt-document",
                blobUri,
                new AnalyzeDocumentOptions { Pages = { "1-50" } });

            // Ожидание завершения анализа
            var result = (await operation.WaitForCompletionAsync()).Value;

            return result;

        }

        private async Task<string?> AnalyzeUserQuery(string userQuery)
        {
            var messages = new List<ChatMessage>
            {
                //ChatMessage.FromSystem("получения ключевых слов запроса."),
                ChatMessage.FromUser($"Get query keywords based on: {userQuery}")
            };

            var completionRequest = new ChatCompletionCreateRequest
            {
                Model = "gpt-4",
                MaxTokens = 100,
                Messages = messages,
                Temperature = (float?)0.5,
                TopP = 1

            };

            var completionResult = await openai.ChatCompletion.CreateCompletion(completionRequest);

            var openAiResponse = completionResult.Choices.First().Message.Content;

            return openAiResponse;
        }

    }
}
