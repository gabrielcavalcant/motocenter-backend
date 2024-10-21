using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
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
        }
    }
}
