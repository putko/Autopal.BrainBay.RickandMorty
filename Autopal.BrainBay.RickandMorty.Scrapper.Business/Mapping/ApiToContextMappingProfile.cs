using AutoMapper;
using Autopal.BrainBay.RickandMorty.Domain.Model;
using ConnectorModel = Autopal.BrainBay.RickandMorty.Scrapper.Connector.Model;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Business.Mapping
{
    internal class ApiToContextMappingProfile : Profile
    {
        /// <summary>
        ///     Default constructor of AutoMapper profile for OffersResponse
        /// </summary>
        public ApiToContextMappingProfile()
        {
            CreateMap<ConnectorModel.Character, Character>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Origin))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species))
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<ConnectorModel.CharacterLocation, Location>().ConvertUsing<LocationConverter>();

            CreateMap<ConnectorModel.Location, Location>()
                .ForMember(dest => dest.Residents, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Dimension, opt => opt.MapFrom(src => src.Dimension))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}