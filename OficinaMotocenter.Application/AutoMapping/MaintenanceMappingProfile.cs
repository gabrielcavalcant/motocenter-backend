using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Maintenance;
using OficinaMotocenter.Application.Dto.Responses.Maintenance;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.AutoMapping
{
    public class MaintenanceMappingProfile : Profile
    {
        public MaintenanceMappingProfile()
        {
            // Mapeamento de Maintenance para MaintenanceDtoResponse
            CreateMap<Maintenance, MaintenanceDtoResponse>();

            // Mapeamento de CreateMaintenanceRequest para Maintenance
            CreateMap<CreateMaintenanceRequest, Maintenance>();

            // Mapeamento de UpdateMaintenanceRequest para Maintenance
            CreateMap<UpdateMaintenanceRequest, Maintenance>();

            // Map Maintenance to MaintenanceDto (for use in paginated responses)
            CreateMap<Maintenance, MaintenanceDtoResponse>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Maintenance>, GetListMaintenanceResponse>()
                .ForMember(dest => dest.Maintenances, opt => opt.MapFrom(src => src)); // Maps List<Maintenance> to List<MaintenanceDtoResponses>'
        }
    }
}
