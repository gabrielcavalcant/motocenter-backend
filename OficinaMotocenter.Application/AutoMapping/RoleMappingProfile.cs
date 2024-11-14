using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
using OficinaMotocenter.Application.Dto.Responses.Role.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Domain.Entities;


namespace OficinaMotocenter.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper Profile for Role entity mappings.
    /// </summary>
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            // Mapeamento de Role para RoleDtoResponse
            CreateMap<Role, RoleDtoResponse>();

            // Mapeamento de CreateRoleRequest para Role
            CreateMap<CreateRoleRequest, Role>();

            // Mapeamento de UpdateRoleRequest para Role
            CreateMap<UpdateRoleRequest, Role>();

            // Map Role to RoleDto (for use in paginated responses)
            CreateMap<Role, RoleDto>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Role>, GetListRoleResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src)); // Maps List<Role> to List<RoleDto>
        }
    }
}
