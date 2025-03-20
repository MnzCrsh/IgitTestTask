using AutoMapper;
using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;
using Igit.Entities.Entities;

namespace Igit.Mapping.Profiles;

internal class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, User>();

        CreateMap<UpdateUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt =>
            {
                opt.Condition(src => src.RoleId != null
                                     && src.RoleId != Guid.Empty);

                opt.MapFrom(src => src.RoleId);
            });

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
    }
}