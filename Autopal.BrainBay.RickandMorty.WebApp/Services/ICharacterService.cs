using System;
using System.Collections;
using System.Collections.Generic;
using Autopal.BrainBay.RickandMorty.Domain.Model;
using Autopal.BrainBay.RickandMorty.WebApp.Models;

namespace Autopal.BrainBay.RickandMorty.WebApp.Services
{
    public interface ICharacterService : IDisposable
    {
        Character FindCharacter(int id);
        Location FindLocation(string name);
        IEnumerable GetCharacterGenders();
        PaginatedItemsViewModel<Character> GetCharactersPaginated(int pageSize, int pageIndex);
        PaginatedItemsViewModel<Location> GetLocationsPaginated(int pageSize, int pageIndex);
        IEnumerable GetCharacterStatuses();
        IList<Location> GetLocations();
        void CreateCharacter(Character Character);
        void UpdateCharacter(Character Character);
        void RemoveCharacter(Character Character);
    }
}