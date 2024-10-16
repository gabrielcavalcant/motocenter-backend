using OficinaMotocenter.Application.Dto.Requests.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Defines a service interface for performing CRUD operations and business logic related to motorcycles.
    /// Inherits from the generic service interface.
    /// </summary>
    public interface IMotorcycleService : IGenericService<Motorcycle>
    {
        Task<CreateMotorcycleResponse> CreateMotorcycleAsync(CreateMotorcycleRequest dto);

        Task<GetMotorcycleByIdResponse> GetMotorcycleByIdAsync(Guid id);
    }
}
