using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
using OficinaMotocenter.Application.Dto.Responses.Role.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IRoleService : IGenericService<Role>
    {
        Task<Role> getByName(string name);

        Task<RoleDtoResponse> GetRoleByIdAsync(Guid roleId);

        Task<GetListRoleResponse> GetListRoleAsync(GetListRoleRequest request, CancellationToken cancellationToken);

        Task<RoleDtoResponse> CreateRoleAsync(CreateRoleRequest request);

        Task<RoleDtoResponse> UpdateRoleAsync(Guid id, UpdateRoleRequest request);

        Task<bool> DeleteRoleAsync(Guid id);
    }
}
