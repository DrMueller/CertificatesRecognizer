using System;
using System.IO;
using System.Threading.Tasks;
using Lamar;
using Microsoft.Extensions.Configuration;
using Mmu.CertificateRecognizer.Areas.UseCase;
using Mmu.CertificateRecognizer.Infrastructure;
using Mmu.CertificateRecognizer.Infrastructure.Settings;

namespace Mmu.CertificateRecognizer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<AppSettings>()
                .Build();

            var settings = new AppSettings();
            configRoot.Bind("AppSettings", settings);
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<Program>();
                    scanner.WithDefaultConventions();
                });

                cfg.For<AppSettings>().Use(settings).Singleton();
            });


            var useCase = container.GetInstance<IRecognizeCertificates>();
            await useCase.ExecuteAsync();

            Console.ReadKey();
        }
    }
}