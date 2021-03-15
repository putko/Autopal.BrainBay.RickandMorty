using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Autopal.BrainBay.RickandMorty.Domain;
using Autopal.BrainBay.RickandMorty.Domain.Model;
using Autopal.BrainBay.RickandMorty.Scrapper.Business.Mapping;
using Autopal.BrainBay.RickandMorty.Scrapper.Connector;
using Microsoft.Extensions.Logging;
using CharacterStatus = Autopal.BrainBay.RickandMorty.Scrapper.Connector.Model.CharacterStatus;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Business
{
    public class ScrapperService : IScrapperService
    {
        private readonly IRickandMortyConnector _connector;
        private readonly ILogger<IScrapperService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ScrapperService(IRickandMortyConnector connector, IUnitOfWork<RickandMortyContext> unitOfWork,
            IMapper mapper,
            ILogger<IScrapperService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _connector = connector;
            _logger = logger;
        }

        public async Task RefreshDbAsync()
        {
            _unitOfWork.RecreateDatabase();
            var locations = await GetAllLocations();
            var characters = await GetAllCharacters(locations);
            _unitOfWork.GetRepository<Character>().Insert(characters);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<IEnumerable<Character>> GetAllCharacters(IEnumerable<Location> locations)
        {
            var characters = await _connector.GetAllCharacters(CharacterStatus.Alive);
            return _mapper.Map<IEnumerable<Character>>(characters,
                opts => opts.Items[LocationConverter.LocationKeyWord] = locations);
        }

        private async Task<IEnumerable<Location>> GetAllLocations()
        {
            var locations = await _connector.GetAllLocations();
            return _mapper.Map<IEnumerable<Location>>(locations);
        }
    }
}