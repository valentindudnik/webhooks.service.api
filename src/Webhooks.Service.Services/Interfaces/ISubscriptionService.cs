using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Models.Result;

namespace Webhooks.Service.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<EntityResult> AddAsync(SubscriptionParameters parameters);
        Task UpdateAsync(Guid subscriptionId, SubscriptionParameters parameters);
        Task DeleteAsync(Guid subscriptionId);
        Task<SubscriptionDto> GetAsync(Guid subscriptionId); 
        Task<IEnumerable<SubscriptionDto>> GetAllAsync();
    }
}
