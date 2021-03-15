using System.Collections.Generic;
using System.Threading.Tasks;
using Autopal.BrainBay.RickandMorty.Scrapper.Connector.Model;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector
{
    public interface IRickandMortyConnector
    {
        /// <summary>
        ///     Get all characters.
        /// </summary>
        /// <returns>Characters enumerable.</returns>
        Task<IEnumerable<Character>> GetAllCharacters(CharacterStatus? statusFilter);

        /// <summary>
        ///     Gets all locatons.
        /// </summary>
        /// <returns>Locations enumarable</returns>
        Task<IEnumerable<Location>> GetAllLocations();
    }
}