using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services.Impllementation
{
    public class OutputWriter : IOutputWriter
    {
        
        public Task WriteAsync(IReadOnlyCollection<RecognizedCertificate> certificates)
        {
            
        }
    }
}
