using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Services;

namespace Mmu.CertificateRecognizer.Areas.UseCase.Implementation
{
    public class RecognizeCertificates : IRecognizeCertificates
    {
        private readonly IRecognizedCertificateFactory _certificateFactory;
        private readonly IOutputWriter _outputWriter;

        public RecognizeCertificates(
            IOutputWriter outputWriter,
            IRecognizedCertificateFactory certificateFactory)
        {
            _outputWriter = outputWriter;
            _certificateFactory = certificateFactory;
        }

        public async Task ExecuteAsync()
        {
            var certificates = await _certificateFactory.CreateAllAsync();
            await _outputWriter.WriteAsync(certificates);
        }
    }
}