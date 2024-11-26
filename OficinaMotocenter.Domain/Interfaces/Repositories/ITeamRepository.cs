using System;
using System.Threading.Tasks;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface for team-specific data access operations, extending generic repository functionalities.
    /// </summary>
    public interface ITeamRepository : IGenericRepository<Team>
    {
        IQueryable<Team> Query();

    }
}