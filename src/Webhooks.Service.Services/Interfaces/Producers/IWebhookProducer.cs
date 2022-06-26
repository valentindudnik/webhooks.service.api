using Webhooks.Service.Models.Events;

namespace Webhooks.Service.Services.Interfaces.Producers
{
    public interface IWebhookProducer
    {
        void Send(WebhookScheduledEvent webhookScheduledEvent);
    }
}
