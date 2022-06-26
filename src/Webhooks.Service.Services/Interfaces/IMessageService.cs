using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Events;

namespace Webhooks.Service.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddAsync(IEnumerable<Message> messages);
        Task SendAsync(WebhookScheduledEvent webhookScheduledEvent);
        HttpRequestMessage CreateRequest(WebhookScheduledEvent webhookScheduledEvent);
    }
}
