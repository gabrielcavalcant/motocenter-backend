using OficinaMotocenter.Domain.Entities.Stock;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines a repository interface for performing CRUD operations on items.
    /// Inherits from the generic repository interface and adds additional methods.
    /// </summary>
    public interface IItemRepository : IGenericRepository<Item>
    {

    }
}
