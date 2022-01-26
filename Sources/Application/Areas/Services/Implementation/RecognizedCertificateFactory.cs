using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;
using Mmu.CertificateRecognizer.Areas.Services.Servants;

namespace Mmu.CertificateRecognizer.Areas.Services.Implementation
{
    public class RecognizedCertificateFactory : IRecognizedCertificateFactory
    {
        private readonly IFormAnalyzer _formAnalyzer;

        public RecognizedCertificateFactory(IFormAnalyzer formAnalyzer)
        {
            _formAnalyzer = formAnalyzer;
        }

        public async Task<IReadOnlyCollection<RecognizedCertificate>> CreateAllAsync()
        {
            const string FilePath = @"C:\MyGit\Personal\Data.Certificates\Weiterbildungen\Microsoft";

            var allFiles = Directory.GetFiles(FilePath, "*.*", SearchOption.AllDirectories);
            var result = new ConcurrentBag<RecognizedCertificate>();

            // Cant use Parallel.ForEach due to the rate limit
            foreach (var file in allFiles)
            {
                Console.WriteLine($"Analyzing {file}..");
                var analysis = await _formAnalyzer.AnalyzeAsync(file);

                var archievementDateKey = analysis.KeyValuePairs.Single(f => f.Key.Content.Contains("Date of achievement"));
                var validUntilKey = analysis.KeyValuePairs.SingleOrDefault(f => f.Key.Content.Contains("Valid until"));

                var archievementDate = DateTime.Parse(archievementDateKey.Value.Content);
                DateTime? validUntil = validUntilKey == null ? null : DateTime.Parse(validUntilKey.Value.Content);
                var certificateName = ParseCertificateName(analysis.Content);

                result.Add(
                    new RecognizedCertificate(
                        file,
                        certificateName,
                        archievementDate,
                        validUntil));
            }

            return result;
        }

        private static int GetStartIndex(string content)
        {
            var index = content.IndexOf("Microsoft Certified Solutions Developer:", StringComparison.Ordinal);
            if (index > 0)
            {
                return index;
            }

            var index2 = content.IndexOf("Microsoft Specialist:", StringComparison.Ordinal);
            if (index2 > 0)
            {
                return index2;
            }

            var index3 = content.IndexOf("Microsoft® Certified Solutions Associate:", StringComparison.Ordinal);
            if (index3 > 0)
            {
                return index3;
            }

            return 0;
        }

        private static string ParseCertificateName(string analysisContent)
        {
            var lines = analysisContent.Split("\n");
            var certNameFromheading = lines[0] == "Microsoft Certified" ? lines[1] : lines[0];

            var generalHeadings = new List<string>
            {
                "Solutions Developer",
                "Microsoft Specialist",
                "Solutions Associate",
            };

            if (!generalHeadings.Contains(certNameFromheading))
            {
                return certNameFromheading;
            }

            var text = analysisContent.Replace("\n", " ");
            var startIndex = GetStartIndex(text);
            var endIndex = text.IndexOf('.', startIndex);

            var str = text.Substring(startIndex, endIndex - startIndex);

            return str;
        }
    }
}