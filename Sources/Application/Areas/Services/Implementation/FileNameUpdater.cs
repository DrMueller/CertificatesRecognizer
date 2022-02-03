using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services.Implementation
{
    public class FileNameUpdater : IFileNameUpdater
    {
        private const string ExpirablePath = "Expirable";
        private const string NonExpirablePath = "NonExpirable";

        public void UpdateFilenames(IReadOnlyCollection<RecognizedCertificate> certificates)
        {
            if (!certificates.Any())
            {
                return;
            }
            
            AssureSubPathsExist();

            foreach (var cert in certificates)
            {
                var newFileName = cert.CertificateName.Replace(" ", "_");
                newFileName = newFileName.Replace("ä", "ae");
                newFileName = newFileName.Replace("ö", "oe");
                newFileName = newFileName.Replace("ü", "ue");
                newFileName = newFileName.Replace("Ä", "Ae");
                newFileName = newFileName.Replace("Ö", "Oe");
                newFileName = string.Concat(newFileName.Split(Path.GetInvalidFileNameChars()));
                newFileName += Path.GetExtension(cert.OriginalFilePath);

                var subPath = cert.HasValidationEndDate ? ExpirablePath : NonExpirablePath;
                var newFilePath = Path.Combine(RecognizedCertificateFactory.FilePath, subPath, newFileName);

                if (!File.Exists(newFilePath))
                {
                    File.Move(cert.OriginalFilePath!, newFilePath);
                }
            }
        }

        private static void AssureSubPathsExist()
        {
            var expirablePath = Path.Combine(RecognizedCertificateFactory.FilePath, ExpirablePath);
            var nonExpirablePath = Path.Combine(RecognizedCertificateFactory.FilePath, NonExpirablePath);

            if (!Directory.Exists(expirablePath))
            {
                Directory.CreateDirectory(expirablePath);
            }

            if (!Directory.Exists(nonExpirablePath))
            {
                Directory.CreateDirectory(nonExpirablePath);
            }
        }
    }
}