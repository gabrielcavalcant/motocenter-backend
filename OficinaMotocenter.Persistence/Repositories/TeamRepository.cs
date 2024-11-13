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
        public TeamRepository(AppDbContext context) : base(context)
        {

        }
    }
}