using AutoMapper;
using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;
using Igit.Entities.Entities;

namespace Igit.Mapping.Profiles;

internal class EnergyBlockMappingProfile : Profile
{
    public EnergyBlockMappingProfile()
    {
        CreateMap<CreateEnergyBlockRequest, EnergyBlock>();

        CreateMap<UpdateEnergyBlockRequest, EnergyBlock>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.StationId, opt =>
            {
                opt.Condition(src => src.StationId != Guid.Empty);
                opt.MapFrom(src => src.StationId);
            });

        CreateMap<EnergyBlock, EnergyBlockResponse>();
    }
}