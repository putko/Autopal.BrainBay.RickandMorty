using System.Threading.Tasks;
using Autopal.BrainBay.RickandMorty.Scrapper.Business;
using Autopal.BrainBay.RickandMorty.Scrapper.Console.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .SetupServices()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogInformation("Starting application");

            var serviceCaller = serviceProvider.GetRequiredService<IScrapperService>();

            await serviceCaller.RefreshDbAsync();

            logger.LogInformation("All done!");
        }
    }
}