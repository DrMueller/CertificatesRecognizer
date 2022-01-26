using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services
{
    public interface IRecognizedCertificateFactory
    {
        Task<IReadOnlyCollection<RecognizedCertificate>> CreateAllAsync();
    }
}
