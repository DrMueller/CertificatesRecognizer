using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services.Implementation
{
    public class OutputWriter : IOutputWriter
    {
        public void Write(IReadOnlyCollection<RecognizedCertificate> certificates)
        {
            var sortedCerts = certificates
                .OrderByDescending(f => f.Issued)
                .ThenByDescending(f => f.ValidTo)
                .ThenBy(f => f.CertificateName)
                .ToList();

            var sb = new StringBuilder();
            foreach (var cert in sortedCerts)
            {
                sb.Append("- ");
                sb.Append(cert.CertificateName);
                sb.Append(", ");
                sb.Append(cert.Issued.ToShortDateString());

                if (cert.ValidTo.HasValue)
                {
                    sb.Append(" - ");
                    sb.Append(cert.ValidTo.Value.ToShortDateString());
                }

                sb.AppendLine();
            }

            var tempFileName = Path.GetTempFileName();
            File.WriteAllText(tempFileName, sb.ToString());
            Process.Start("notepad.exe", tempFileName);
        }
    }
}