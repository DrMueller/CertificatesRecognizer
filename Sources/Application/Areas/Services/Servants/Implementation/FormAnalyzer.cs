using System;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Mmu.CertificateRecognizer.Infrastructure.Settings;

namespace Mmu.CertificateRecognizer.Areas.Services.Servants.Implementation
{
    public class FormAnalyzer : IFormAnalyzer
    {
        private readonly AppSettings _appSettings;

        public FormAnalyzer(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<AnalyzeResult> AnalyzeAsync(string filePath)
        {
            var credential = new AzureKeyCredential(_appSettings.ApiKey);
            var client = new DocumentAnalysisClient(new Uri(_appSettings.ApiEndpoint), credential);

            await using var fs = File.OpenRead(filePath);
            var operation = await client.StartAnalyzeDocumentAsync("prebuilt-document", fs);
            await operation.WaitForCompletionAsync();

            return operation.Value;
        }
    }
}