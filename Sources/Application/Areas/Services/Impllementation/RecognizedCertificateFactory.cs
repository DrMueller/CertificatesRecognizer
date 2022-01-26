using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;
using Mmu.CertificateRecognizer.Areas.Services.Servants;

namespace Mmu.CertificateRecognizer.Areas.Services.Impllementation
{
    public class RecognizedCertificateFactory : IRecognizedCertificateFactory
    {
        private readonly IFormAnalyzer _formAnalyzer;

        public RecognizedCertificateFactory(IFormAnalyzer formAnalyzer)
        {
            _formAnalyzer = formAnalyzer;
        }

        public Task<IReadOnlyCollection<RecognizedCertificate>> CreateAllAsync()
        {
            
        }
    }
}
