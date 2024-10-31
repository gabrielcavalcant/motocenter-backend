using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IRoleService : IGenericService<Role>
    {
        Task<Role> getByName(string name);
    }
}
