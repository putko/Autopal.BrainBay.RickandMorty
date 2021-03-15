namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client.Model
{
    internal class PageInfo
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }
}