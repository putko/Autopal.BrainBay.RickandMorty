using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client;
using Autopal.BrainBay.RickandMorty.Scrapper.Connector.Model;
using Microsoft.Extensions.Logging;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector
{
    public class RickandMortyConnector : ConnectorBase, IRickandMortyConnector
    {
        public RickandMortyConnector(HttpClient httpClient, ILogger<RickandMortyConnector> logger, IMapper mapper) :
            base(httpClient, logger, mapper)
        {
        }

        public async Task<IEnumerable<Character>> GetAllCharacters(CharacterStatus? statusFilter)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append("api/character/?");
            if (statusFilter.HasValue)
                urlBuilder.Append(Uri.EscapeDataString("status") + "=")
                    .Append(Uri.EscapeDataString(ConvertToString(statusFilter, CultureInfo.InvariantCulture)))
                    .Append("&");

            urlBuilder.Length--;

            var dto = await GetPages<Client.Model.Character>(urlBuilder.ToString());

            return Mapper.Map<IEnumerable<Character>>(dto);
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            var dto = await GetPages<Client.Model.Location>("api/location/");
            return Mapper.Map<IEnumerable<Location>>(dto);
        }
    }
}