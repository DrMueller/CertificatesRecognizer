using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Models;

namespace Mmu.CertificateRecognizer.Areas.Services.Implementation
{
    public class FileNameUpdater : IFileNameUpdater
    {
        public Task UpdateFilenamesAsync(IReadOnlyCollection<RecognizedCertificate> certificates)
        {
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

                var newFilePath = cert.OriginalFilePath!.Replace(Path.GetFileName(cert.OriginalFilePath)!, newFileName);

                if (!File.Exists(newFilePath))
                {
                    File.Move(cert.OriginalFilePath!, newFilePath);
                }
            }

            return Task.CompletedTask;
        }
    }
}