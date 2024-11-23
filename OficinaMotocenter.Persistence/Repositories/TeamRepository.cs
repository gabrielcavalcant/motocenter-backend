using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Repository specific for the <see cref="Team"/> entity.
    /// </summary>
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly AppDbContext _appDbContext;

        public TeamRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<Team> GetByIdAsync(Guid id)
        {
            return await _context.Teams
                .Include(t => t.Members) // Carrega os membros associados
                .FirstOrDefaultAsync(t => t.TeamId == id);
        }

        public IQueryable<Team> Query()
        {
            return _appDbContext.Teams.AsQueryable();
        }
    }
}