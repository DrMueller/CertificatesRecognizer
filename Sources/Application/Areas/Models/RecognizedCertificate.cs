using System;

namespace Mmu.CertificateRecognizer.Areas.Models
{
    public class RecognizedCertificate
    {
        public string CertificateName { get; }
        public DateTime Issued { get; }
        public string OriginalFilePath { get; }
        public DateTime? ValidTo { get; }

        public bool HasValidationEndDate => ValidTo.HasValue;

        public RecognizedCertificate(
            string originalFilePath,
            string certificateName,
            DateTime issued,
            DateTime? validTo)
        {
            OriginalFilePath = originalFilePath;
            CertificateName = certificateName;
            Issued = issued;
            ValidTo = validTo;
        }
    }
}