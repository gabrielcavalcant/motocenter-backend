using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities.Stock;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Repository specific for the <see cref="Item"/> entity.
    /// </summary>
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ItemRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        ///// <summary>
        ///// Retrieves a customer by their CPF.
        ///// </summary>
        ///// <param name="cpf">The CPF of the customer to retrieve.</param>
        ///// <returns>The customer corresponding to the provided CPF.</returns>
        //public async Task AddItemAsync(IList<Item> itens)
        //{
        //    foreach(Item item in itens)
        //    {
        //        return await _context.Set<Item>().FirstOrDefaultAsync(item => item.Cpf == cpf);

        //    }
        //}
    }
}
