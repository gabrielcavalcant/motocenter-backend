using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.User;
using OficinaMotocenter.Application.Dto.Responses.User;
using OficinaMotocenter.Domain.Entities;


namespace OficinaMotocenter.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper Profile for User entity mappings.
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Mapeamento de User para UserDTO
            CreateMap<User, UserDtoResponse>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null)); // Mapeia o nome da Role se ela existir

            // Mapeamento de UserDTO para User
            CreateMap<UserDtoResponse, User>();

            // Mapeamento de CreateUserRequest para User
            CreateMap<CreateUserRequest, User>();

            // Mapeamento de UpdateUserRequest para User (somente as propriedades que não são nulas)
            CreateMap<UpdateUserRequest, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ignora propriedades nulas
        }
    }
}
