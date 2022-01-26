using System;
using System.Threading.Tasks;
using Mmu.CertificateRecognizer.Areas.Services;

namespace Mmu.CertificateRecognizer.Areas.UseCase.Implementation
{
    public class RecognizeCertificates : IRecognizeCertificates
    {
        private readonly IRecognizedCertificateFactory _certificateFactory;
        private readonly IFileNameUpdater _fileNameUpdater;
        private readonly IOutputWriter _outputWriter;

        public RecognizeCertificates(
            IOutputWriter outputWriter,
            IRecognizedCertificateFactory certificateFactory,
            IFileNameUpdater fileNameUpdater)
        {
            _outputWriter = outputWriter;
            _certificateFactory = certificateFactory;
            _fileNameUpdater = fileNameUpdater;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Creating certificates..");
            var certificates = await _certificateFactory.CreateAllAsync();

            Console.WriteLine("Writing output..");
            _outputWriter.Write(certificates);

            Console.WriteLine("Updating filenames..");
            await _fileNameUpdater.UpdateFilenamesAsync(certificates);

            Console.WriteLine("Finished.");
        }
    }
}