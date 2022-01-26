using System.Threading.Tasks;

namespace Mmu.CertificateRecognizer.Areas.UseCase
{
    public interface IRecognizeCertificates
    {
        Task ExecuteAsync();
    }
}