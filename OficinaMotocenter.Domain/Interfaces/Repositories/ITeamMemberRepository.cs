using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface for team member-specific data access operations, extending generic repository functionalities.
    /// </summary>
    public interface ITeamMemberRepository : IGenericRepository<TeamMember>
    {

        
    }
}