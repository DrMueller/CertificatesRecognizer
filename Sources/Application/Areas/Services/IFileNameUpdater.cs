using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services
{
    public interface IFileNameUpdater
    {
        void UpdateFilenames(IReadOnlyCollection<RecognizedCertificate> certificates);
    }
}