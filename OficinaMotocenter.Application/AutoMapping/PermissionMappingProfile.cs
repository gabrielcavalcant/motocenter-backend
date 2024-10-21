using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Domain.Entities;


namespace OficinaMotocenter.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper Profile for Permission entity mappings.
    /// </summary>
    public class PermissionMappingProfile : Profile
    {
        public PermissionMappingProfile()
        {
            // Mapeamento de Permission para PermissionDtoResponse
            CreateMap<Permission, PermissionDtoResponse>();

            // Mapeamento de CreatePermissionDTO para Permission
            CreateMap<CreatePermissionRequest, Permission>();

            // Mapeamento de UpdatePermissionDTO para Permission
            CreateMap<UpdatePermissionRequest, Permission>();

            //// Mapeamento de CreateRoleDTO para Role
            //CreateMap<CreateRoleDTO, Role>();

            //// Mapeamento de UpdateRoleDTO para Role
            //CreateMap<UpdateRoleDTO, Role>();
        }
    }
}
