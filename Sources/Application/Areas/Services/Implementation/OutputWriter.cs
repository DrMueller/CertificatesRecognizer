using System.Collections.Generic;
using System.ComponentModel.Design;
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
            if (!certificates.Any())
            {
                return;
            }

            var sb = new StringBuilder();

            var sortedCerts = certificates
                .OrderByDescending(f => f.Issued)
                .ThenByDescending(f => f.ValidTo)
                .ThenBy(f => f.CertificateName)
                .ToList();

            var grpdCerts = sortedCerts.GroupBy(f => f.HasValidationEndDate);
            foreach(var grp in grpdCerts)
            {
                sb.AppendLine(grp.Key ? "# Expirable" : "# Non-expirable");
                sb.AppendLine();
                Append(sb, grp);
                sb.AppendLine();
                sb.AppendLine();
            }


            var tempFileName = Path.GetTempFileName();
            File.WriteAllText(tempFileName, sb.ToString());
            Process.Start("notepad.exe", tempFileName);
        }

        private static void Append(StringBuilder sb, IEnumerable<RecognizedCertificate> certs)
        {
            foreach (var cert in certs)
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

        }
    }
}