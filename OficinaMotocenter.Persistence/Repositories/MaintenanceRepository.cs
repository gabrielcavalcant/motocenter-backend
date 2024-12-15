using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Repository specific for the <see cref="Maintenance"/> entity.
    /// </summary>
    public class MaintenanceRepository : GenericRepository<Maintenance>, IMaintenanceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MaintenanceRepository(AppDbContext context) : base(context)
        {
            // Add specific methods for Maintenance here if needed
        }
    }
}
