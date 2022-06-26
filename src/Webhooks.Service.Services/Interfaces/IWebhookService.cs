using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Models.Result;

namespace Webhooks.Service.Services.Interfaces
{
    public interface IWebhookService
    {
        Task<EntityResult> AddAsync(WebhookParameters parameters);
        Task UpdateAsync(Guid webhookId, WebhookParameters parameters);
        Task DeleteAsync(Guid webhookId);
        Task<WebhookDto> GetAsync(Guid webhookId);
        Task<IEnumerable<WebhookDto>> GetAllAsync();
    }
}
