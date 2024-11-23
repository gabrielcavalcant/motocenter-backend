    using AutoMapper;
    using OficinaMotocenter.Application.Dto.Requests.TeamMember;
    using OficinaMotocenter.Application.Dto.Responses.TeamMember;
    using OficinaMotocenter.Application.Dto.Responses.TeamMember.OficinaMotocenter.Application.Dto.Responses;
    using OficinaMotocenter.Domain.Entities;

    namespace OficinaMotocenter.Application.AutoMapping
    {
        /// <summary>
        /// AutoMapper Profile for TeamMember entity mappings.
        /// </summary>
        public class TeamMemberMappingProfile : Profile
        {
            public TeamMemberMappingProfile()
            {
                // Mapeamento de TeamMember para TeamMemberDtoResponse
                CreateMap<TeamMember, TeamMemberDtoResponse>();

                // Mapeamento de CreateTeamMemberRequest para TeamMember
                CreateMap<CreateTeamMemberRequest, TeamMember>();

                // Mapeamento de UpdateTeamMemberRequest para TeamMember
                CreateMap<UpdateTeamMemberRequest, TeamMember>();

                // Map Motorcycle to MotorcycleDto (for use in paginated responses)
                CreateMap<TeamMember, TeamMemberDtoResponse>();

                // Optional: Mapping for paginated responses if needed
                CreateMap<List<TeamMember>, GetListTeamMemberResponse>()
                    .ForMember(dest => dest.TeamMember, opt => opt.MapFrom(src => src)); // Maps List<Permission> to List<PermissionDto>
            }
        }
    }