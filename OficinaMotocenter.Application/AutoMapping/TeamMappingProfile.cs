using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Team;
using OficinaMotocenter.Application.Dto.Responses.Team;
using OficinaMotocenter.Application.Dto.Responses.Team.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper Profile for Team entity mappings.
    /// </summary>
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile()
        {
            // Mapeamento de Team para TeamDtoResponse
            CreateMap<Team, TeamDtoResponse>()
                .ForMember(dest => dest.TeamMembers, opt => opt.MapFrom(src => src.Members));

            // Mapeamento de CreateTeamRequest para Team
            CreateMap<CreateTeamRequest, Team>();

            // Mapeamento de UpdateTeamRequest para Team
            CreateMap<UpdateTeamRequest, Team>();

            // Map Motorcycle to MotorcycleDto (for use in paginated responses)
            CreateMap<Team, TeamDtoResponse>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Team>, GetListTeamResponse>()
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src)); // Maps List<Permission> to List<PermissionDto>
        }
    }
}