using System.Threading.Tasks;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Business
{
    public interface IScrapperService
    {
        Task RefreshDbAsync();
    }
}