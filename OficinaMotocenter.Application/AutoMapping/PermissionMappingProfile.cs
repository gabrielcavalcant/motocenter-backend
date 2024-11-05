using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
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

            // Map Motorcycle to MotorcycleDto (for use in paginated responses)
            CreateMap<Permission, PermissionDto>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Permission>, GetListPermissionResponse>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src)); // Maps List<Permission> to List<PermissionDto>
        }
    }
}
