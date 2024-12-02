using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using OficinaMotocenter.Domain.Entities;


namespace OficinaMotocenter.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper Profile for Motorcycle entity mappings.
    /// </summary>
    public class MotorcycleMappingProfile : Profile
    {
        public MotorcycleMappingProfile()
        {
            // Map CreateMotorcycleRequest to Motorcycle
            CreateMap<CreateMotorcycleRequest, Motorcycle>();
            // Map UpdateMotorcycleRequest to Motorcycle (for updates)
            CreateMap<UpdateMotorcycleRequest, Motorcycle>();
            // Map LinkMotorcycleToCustomerRequest to Motorcycle (for linking to a customer)
            CreateMap<LinkMotorcycleToCustomerRequest, Motorcycle>();

            CreateMap<Motorcycle, MotorcycleDtoResponse>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Motorcycle>, GetListMotorcycleResponse>()
                .ForMember(dest => dest.Motorcycles, opt => opt.MapFrom(src => src)); // Maps List<Motorcycle> to List<MotorcycleDto>
        }
    }
}
