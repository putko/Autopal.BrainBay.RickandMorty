using System.Threading.Tasks;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Console.ServiceProxy
{
    public interface IScrapperServiceProxy
    {
        Task ResetDb();
    }
}