using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services
{
    public interface IRecognizedCertificateFactory
    {
        Task<IReadOnlyCollection<RecognizedCertificate>> CreateAllAsync();
    }
}