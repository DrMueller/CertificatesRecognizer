using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmu.CertificateRecognizer.Areas.Models
{ 
    public class RecognizedCertificate
    {
        public string Name { get; }
        public DateTime Issued { get; }
        public DateTime? ValidTo { get; }

        public RecognizedCertificate(string name, DateTime issued, DateTime? validTo)
        {
            Name = name;
            Issued = issued;
            ValidTo = validTo;
        }
    }
}
