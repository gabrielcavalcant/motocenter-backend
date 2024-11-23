using AutoMapper;
using OficinaMotocenter.Application.Dto.Requests.Team;
using OficinaMotocenter.Application.Dto.Responses.Team;
using OficinaMotocenter.Application.Dto.Responses.Team.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Dto.Responses.TeamMember;
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
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members));

            // Mapeamento de CreateTeamRequest para Team
            CreateMap<CreateTeamRequest, Team>();

            // Mapeamento de UpdateTeamRequest para Team
            CreateMap<UpdateTeamRequest, Team>();

            // Map Motorcycle to MotorcycleDto (for use in paginated responses)
            CreateMap<Team, TeamDtoResponse>();

            // Optional: Mapping for paginated responses if needed
            CreateMap<List<Team>, GetListTeamResponse>()
                .ForMember(dest => dest.Teams, opt => opt.MapFrom(src => src)); // Maps List<Permission> to List<PermissionDto>

            CreateMap<Team, TeamDtoResponse>();
            CreateMap<TeamMember, TeamMemberDtoResponse>()
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty.ToString()));
        }
    }
}