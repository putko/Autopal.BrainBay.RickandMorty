using System.Linq;
using AutoMapper;
using ClientModel = Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client.Model;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector.Model.Mapping
{
    internal class RickAndMortyMappingProfile : Profile
    {
        /// <summary>
        ///     Default constructor of AutoMapper profile for OffersResponse
        /// </summary>
        public RickAndMortyMappingProfile()
        {
            CreateMap<ClientModel.CharacterLocation, CharacterLocation>()
                .ConstructUsing(cls =>
                    new CharacterLocation(cls.Name, cls.Url.ToUri()));

            CreateMap<ClientModel.CharacterOrigin, CharacterLocation>()
                .ConstructUsing(cls =>
                    new CharacterLocation(cls.Name, cls.Url.ToUri()));

            CreateMap<ClientModel.Character, Character>()
                .ConstructUsing(cls =>
                    new Character(cls.Id, cls.Name, cls.Status.ToEnum<CharacterStatus>(),
                        cls.Species, cls.Type, cls.Gender.ToEnum<CharacterGender>(),
                        new CharacterLocation(cls.Location.Name, cls.Location.Url.ToUri()),
                        new CharacterLocation(cls.Origin.Name, cls.Origin.Url.ToUri()),
                        cls.Url.ToUri(), cls.Episode.Select(x => x.ToUri()).ToList(),
                        cls.Url.ToUri(), cls.Created.ToDateTime()));


            CreateMap<ClientModel.Location, Location>()
                .ConstructUsing(cls =>
                    new Location(cls.Id, cls.Name, cls.Type, cls.Dimension,
                        cls.Residents.Select(x => x.ToUri()).ToList(), cls.Url.ToUri(),
                        cls.Created.ToDateTime()));

            CreateMap<ClientModel.Episode, Episode>()
                .ConstructUsing(cls =>
                    new Episode(cls.Id, cls.Name, cls.Air_date.ToDateTime(), cls.EpisodeInformation,
                        cls.Characters.Select(x => x.ToUri()).ToList(), cls.Url.ToUri(), cls.Created.ToDateTime()));

            AllowNullCollections = true;
        }
    }
}