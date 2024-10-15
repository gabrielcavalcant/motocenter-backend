using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Customer;
using OficinaMotocenter.Application.Dto.Responses.Customer;
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

            // Map Customer to CreateCustomerResponse
            CreateMap<Customer, CreateCustomerResponse>();
            CreateMap<Customer, GetCustomerByIdResponse>();
        }
    }
}
