using Microsoft.Extensions.Logging;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using AutoMapper;
using OficinaMotocenter.Application.Dto.Responses.TeamMember;
using OficinaMotocenter.Application.Dto.Requests.TeamMember;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using OficinaMotocenter.Application.Dto.Responses.TeamMember.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Exceptions;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service implementation for team member-specific operations.
    /// Inherits basic CRUD operations from the generic service.
    /// </summary>
    public class TeamMemberService : GenericService<TeamMember>, ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly ILogger<TeamMemberService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberService"/> class.
        /// </summary>
        /// <param name="teamMemberRepository">The repository for team member operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        /// <param name="mapper">The auto mapper instance for mapping object operations.</param>
        /// <param name="unitOfWork">The unit of work for team member operations.</param>
        public TeamMemberService(ITeamMemberRepository teamMemberRepository, ILogger<TeamMemberService> logger, IMapper mapper, IUnitOfWork unitOfWork, IUserService userService)
            : base(teamMemberRepository, unitOfWork, logger)
        {
            _teamMemberRepository = teamMemberRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        /// <summary>
        /// Executes the creation of a new team member.
        /// </summary>
        /// <param name="request">The request DTO containing team member information.</param>
        /// <returns>A response DTO with the details of the created team member.</returns>
        public async Task<TeamMemberDtoResponse> CreateTeamMemberAsync(CreateTeamMemberRequest request)
        {
            User user = await _userService.GetByIdAsync(request.UserId);
            if (user == null) 
            {
                _logger.LogWarning("User not found", request.UserId);
                throw new InvalidArgumentException("User not found");
            }
            TeamMember teamMember = _mapper.Map<TeamMember>(request);
            _logger.LogInformation("Creating team member: {@teamMember}", teamMember);
            TeamMember createdTeamMember = await CreateAsync(teamMember);
            return _mapper.Map<TeamMemberDtoResponse>(createdTeamMember);
        }

        /// <summary>
        /// Executes the retrieval of a team member by its ID.
        /// </summary>
        /// <param name="teamMemberId">The unique ID of the team member to retrieve.</param>
        /// <returns>A response DTO with the details of the team member.</returns>
        public async Task<TeamMemberDtoResponse> GetTeamMemberByIdAsync(Guid teamMemberId)
        {
            _logger.LogInformation("Searching team member using GUID: {@id}", teamMemberId);
            TeamMember teamMember = await GetByIdAsync(teamMemberId);
            return _mapper.Map<TeamMemberDtoResponse>(teamMember);
        }

        /// <summary>
        /// Executes the retrieval of all team members with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of team members and pagination details.</returns>
        public async Task<GetListTeamMemberResponse> GetListTeamMemberAsync(GetListTeamMemberRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get team member list using: {@request}", request);

            IList<TeamMember> teamMemberList = await GetAllAsync(
                cancellationToken,
                filter: m => (!request.Specialty.HasValue || m.Specialty == request.Specialty),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );
            GetListTeamMemberResponse response = _mapper.Map<GetListTeamMemberResponse>(teamMemberList);
            response.TotalCount = teamMemberList.Count;
            return response;
        }

        /// <summary>
        /// Executes the update of an existing team member.
        /// </summary>
        /// <param name="teamMemberId">The unique ID of the team member.</param>
        /// <param name="request">The request DTO containing team member information.</param>
        /// <returns>A response DTO with the details of the updated team member.</returns>
        public async Task<TeamMemberDtoResponse> UpdateTeamMemberAsync(Guid teamMemberId, UpdateTeamMemberRequest request)
        {
            TeamMember teamMember = await GetByIdAsync(teamMemberId);
            _mapper.Map(request, teamMember);
            TeamMember updatedTeamMember = await UpdateAsync(teamMember);
            updatedTeamMember = await GetByIdAsync(updatedTeamMember.TeamMemberId);
            TeamMemberDtoResponse response = _mapper.Map<TeamMemberDtoResponse>(updatedTeamMember);
            return response;
        }

        /// <summary>
        /// Executes the soft delete of an existing team member.
        /// </summary>
        /// <param name="teamMemberId">The unique ID of the team member.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteTeamMemberAsync(Guid teamMemberId)
        {
            bool teamMemberDeleted = await DeleteAsync(teamMemberId);
            return teamMemberDeleted;
        }
    }
}
