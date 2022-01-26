using System.Threading.Tasks;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace Mmu.CertificateRecognizer.Areas.Services.Servants
{
    public interface IFormAnalyzer
    {
        Task<AnalyzeResult> AnalyzeAsync(string filePath);
    }
}