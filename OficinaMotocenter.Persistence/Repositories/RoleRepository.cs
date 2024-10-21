using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Repository specific for the <see cref="Role"/> entity.
    /// </summary>
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RoleRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        /// <summary>
        /// Retrieves a Role by their name.
        /// </summary>
        /// <param name="name">The name of the Role to retrieve.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The Role corresponding to the provided name.</returns>
        public async Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
