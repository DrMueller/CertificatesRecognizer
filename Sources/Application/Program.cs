using System;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.Extensions.Configuration;
using Mmu.CertificateRecognizer.Infrastructure;

namespace Mmu.CertificateRecognizer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<AppSettings>()
                .Build();

            var settings = new AppSettings();
            configRoot.Bind("AppSettings", settings);

            var credential = new AzureKeyCredential(settings.ApiKey);
            var client = new DocumentAnalysisClient(new Uri(settings.ApiEndpoint), credential);

            var allFiles = Directory.GetFiles(@"D:\MyGit\Personal\Data.Certificates\Weiterbildungen\Microsoft");

            foreach (var file in allFiles)
                using (var fs = File.OpenRead(file))
                {
                    var operation = await client.StartAnalyzeDocumentAsync("prebuilt-document", fs);
                    await operation.WaitForCompletionAsync();

                    var result = operation.Value;

                    Console.WriteLine("Detected key-value pairs:");

                    foreach (var kvp in result.KeyValuePairs)
                        if (kvp.Value == null)
                            Console.WriteLine($"  Found key with no value: '{kvp.Key.Content}'");
                        else
                            Console.WriteLine($"  Found key-value pair: '{kvp.Key.Content}' and '{kvp.Value.Content}'");

                }

            Console.ReadKey();
        }
    }
}