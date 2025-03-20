using AutoMapper;
using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;
using Igit.Entities.Entities;

namespace Igit.Mapping.Profiles;

internal class StationMappingProfile : Profile
{
    public StationMappingProfile()
    {
        CreateMap<CreateStationRequest, Station>();

        CreateMap<UpdateStationRequest, Station>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.EnergyBlocks, opt =>
            {
                opt.Condition(src => src.EnergyBlocks != null);
                opt.MapFrom(src => src.EnergyBlocks);
            });

        CreateMap<Station, StationResponse>();
    }
}