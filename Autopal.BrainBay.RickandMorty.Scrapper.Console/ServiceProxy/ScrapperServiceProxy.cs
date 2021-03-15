using System.Threading.Tasks;
using Autopal.BrainBay.RickandMorty.Scrapper.Business;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Console.ServiceProxy
{
    public class ScrapperServiceProxy : IScrapperServiceProxy
    {
        private readonly IScrapperService _service;

        public ScrapperServiceProxy(IScrapperService service)
        {
            _service = service;
        }

        public async Task ResetDb()
        {
            await _service.RefreshDbAsync();
        }
    }
}