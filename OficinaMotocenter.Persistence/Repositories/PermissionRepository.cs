using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Repository specific for the <see cref="Permission"/> entity.
    /// </summary>
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PermissionRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        /// <summary>
        /// Retrieves a permission by their name.
        /// </summary>
        /// <param name="name">The name of the permission to retrieve.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The permission corresponding to the provided name.</returns>
        public async Task<Permission> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
