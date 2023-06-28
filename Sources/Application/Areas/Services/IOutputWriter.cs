using System.Collections.Generic;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services
{
    public interface IOutputWriter
    {
        void Write(IReadOnlyCollection<RecognizedCertificate> certificates);
    }
}