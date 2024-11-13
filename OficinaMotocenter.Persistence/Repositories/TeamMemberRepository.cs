using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Repository specific for the <see cref="TeamMember"/> entity.
    /// </summary>
    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(AppDbContext context) : base(context)
        {

        }

    }
}