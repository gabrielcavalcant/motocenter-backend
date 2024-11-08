using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
