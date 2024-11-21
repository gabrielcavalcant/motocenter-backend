using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Entities.Stock;
using OficinaMotocenter.Application.Dto.Responses.Item;
using OficinaMotocenter.Application.Dto.Requests.Item;

namespace OficinaMotocenter.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper Profile for Item entity mappings.
    /// </summary>
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            // Mapeamento de Item para ItemDtoResponse
            CreateMap<Item, ItemDtoResponse>();

            // Mapeamento de CreateItemRequest para Item
            CreateMap<CreateItemRequest, Item>();

            // Mapeamento de UpdateItemRequest para Item
            CreateMap<UpdateItemRequest, Item>();

            // Map Motorcycle to MotorcycleDto (for use in paginated responses)
            CreateMap<Item, ItemDtoResponse>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Item>, GetListItemResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src)); // Maps List<Item> to List<ItemDtoResponse>
        }
    }
}
