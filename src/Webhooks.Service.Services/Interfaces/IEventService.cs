using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Models.Result;

namespace Webhooks.Service.Services.Interfaces
{
    public interface IEventService
    {
        Task<EntityResult> AddAsync(EventParameters parameters);
        Task UpdateAsync(Guid eventId, EventParameters parameters);
        Task DeleteAsync(Guid eventId);
        Task<EventDto> GetAsync(Guid eventId);
        Task<IEnumerable<EventDto>> GetAllAsync();
    }
}
