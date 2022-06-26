using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;

namespace Webhooks.Service.Services.Interfaces
{
    public interface IWebhookService
    {
        Task AddAsync(WebhookParameters parameters);
        Task UpdateAsync(Guid webhookId, WebhookParameters parameters);
        Task DeleteAsync(Guid webhookId);
        Task<WebhookDto> GetAsync(Guid webhookId);
        Task<IEnumerable<WebhookDto>> GetAllAsync();
    }
}
