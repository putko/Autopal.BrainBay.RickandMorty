using System.Collections.Generic;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client.Model
{
    internal class Page<T>
    {
        public PageInfo Info { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}