using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;

namespace Webhooks.Service.Services.Interfaces
{
    public interface IEventService
    {
        Task AddAsync(EventParameters parameters);
        Task UpdateAsync(Guid eventId, EventParameters parameters);
        Task DeleteAsync(Guid eventId);
        Task<EventDto> GetAsync(Guid eventId);
        Task<IEnumerable<EventDto>> GetAllAsync();
    }
}
