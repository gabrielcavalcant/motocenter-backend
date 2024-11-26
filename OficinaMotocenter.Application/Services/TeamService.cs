using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Team;
using OficinaMotocenter.Application.Dto.Responses.Team;
using OficinaMotocenter.Application.Dto.Responses.Team.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using System.Linq.Expressions;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service for managing teams, including CRUD operations and team member association.
    /// </summary>
    public class TeamService : GenericService<Team>, ITeamService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeamService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamService"/> class.
        /// </summary>
        /// <param name="teamMemberRepository">Repository for accessing team member data.</param>
        /// <param name="teamRepository">Repository for accessing team data.</param>
        /// <param name="unitOfWork">Unit of work for managing transactions.</param>
        /// <param name="mapper">Mapper for entity-DTO conversions.</param>
        /// <param name="logger">Logger for logging operations.</param>
        public TeamService(ITeamMemberRepository teamMemberRepository,
                           ITeamRepository teamRepository,
                           IUnitOfWork unitOfWork,
                           IMapper mapper,
                           ILogger<TeamService> logger) : base(teamRepository, unitOfWork, logger)
        {
            _teamMemberRepository = teamMemberRepository;
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new team based on the provided details.
        /// </summary>
        /// <param name="request">Details of the team to create.</param>
        /// <returns>A <see cref="TeamDtoResponse"/> containing the created team details.</returns>
        public async Task<TeamDtoResponse> CreateTeamAsync(CreateTeamRequest request)
        {
            Team newTeam = _mapper.Map<Team>(request);
            Team createdTeam = await CreateAsync(newTeam);
            return _mapper.Map<TeamDtoResponse>(createdTeam);
        }

        /// <summary>
        /// Updates an existing team based on its identifier and update details.
        /// </summary>
        /// <param name="id">Unique identifier of the team to update.</param>
        /// <param name="request">Updated team details.</param>
        /// <returns>A <see cref="TeamDtoResponse"/> containing the updated team details.</returns>
        public async Task<TeamDtoResponse> UpdateTeamAsync(Guid id, UpdateTeamRequest request)
        {
            Team teamToUpdate = await GetByIdAsync(id);

            if (teamToUpdate == null)
            {
                throw new InvalidArgumentException("Team not found");
            }

            _mapper.Map(request, teamToUpdate);
            await UpdateAsync(teamToUpdate);
            return _mapper.Map<TeamDtoResponse>(teamToUpdate);
        }

        /// <summary>
        /// Associates a team member with a specific team.
        /// </summary>
        /// <param name="teamId">Unique identifier of the team.</param>
        /// <param name="teamMemberId">Unique identifier of the team member to associate.</param>
        /// <returns>A boolean indicating success or failure of the association.</returns>
        public async Task<bool> AddMemberToTeam(Guid teamId, Guid teamMemberId)
        {
            Team team = await GetByIdAsync(teamId);

            if (team == null)
            {
                return false;
            }

            TeamMember teamMember = await _teamMemberRepository.GetByIdAsync(teamMemberId);
            if (teamMember == null)
            {
                return false;
            }

            team.Members.Add(teamMember);
            await _teamRepository.UpdateAsync(team);
            await _unitOfWork.Commit();

            return true;
        }

        /// <summary>
        /// Retrieves a team by its unique identifier.
        /// </summary>
        /// <param name="teamId">Unique identifier of the team.</param>
        /// <returns>A <see cref="TeamDtoResponse"/> with team details, or null if not found.</returns>
        public async Task<TeamDtoResponse> GetTeamByIdAsync(Guid teamId)
        {
            Team team = await GetByIdAsync(teamId);
            return _mapper.Map<TeamDtoResponse>(team);
        }

        /// <summary>
        /// Retrieves a paginated list of teams based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListTeamResponse"/> containing the filtered teams and total count.</returns>
        public async Task<GetListTeamResponse> GetListTeamAsync(GetListTeamRequest request, CancellationToken cancellationToken)
        {
            var query = _teamRepository.Query()
                .Include(t => t.Members)
                .ThenInclude(m => m.User); // Inclua outros dados relacionados, se necessário.

            // Filtrar, paginar ou ordenar, se necessário.
            var teams = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var response = new GetListTeamResponse
            {
                Teams = _mapper.Map<List<TeamDtoResponse>>(teams),
                TotalCount = await query.CountAsync(cancellationToken)
            };

            return response;
        }

        /// <summary>
        /// Executes the soft delete of an existing team.
        /// </summary>
        /// <param name="teamId">Unique identifier of the team to delete.</param>
        /// <returns>A boolean result indicating success or failure of the deletion.</returns>
        public async Task<bool> DeleteTeamAsync(Guid teamId)
        {
            bool teamDeleted = await base.DeleteAsync(teamId);
            return teamDeleted;
        }
    }
}