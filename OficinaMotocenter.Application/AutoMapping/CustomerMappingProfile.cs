using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Customer;
using OficinaMotocenter.Application.Dto.Requests.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Customer;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper Profile for Customer entity mappings.
    /// </summary>
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // Map CreateCustomerRequest to Customer
            CreateMap<CreateCustomerRequest, Customer>();
            // Map UpdateCustomerRequest to Customer (for updates)
            CreateMap<UpdateCustomerRequest, Customer>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



            // Map Customer to CreateCustomerResponse
            CreateMap<Customer, CreateCustomerResponse>();
            CreateMap<Customer, GetCustomerByIdResponse>();
            CreateMap<Customer, GetCustomerByIdResponse>();
            CreateMap<Customer, UpdateCustomerResponse>();

            // Map Customer to CustomerDto (for use in paginated responses)
            CreateMap<Customer, CustomerDto>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Customer>, GetListCustomerResponse>()
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src)); // Maps List<Customer> to List<CustomerDto>
        }
    }
}
