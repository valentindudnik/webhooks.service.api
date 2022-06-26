using Microsoft.Extensions.Logging;
using Webhooks.RabbitMQ.Client.Interfaces;
using Webhooks.RabbitMQ.Client.Producers;
using Webhooks.RabbitMQ.Models.Common;
using Webhooks.Service.Models.Events;
using Webhooks.Service.Services.Interfaces.Producers;

namespace Webhooks.Service.Services.Producers
{
    public class WebhookProducer : RabbitMQProducer, IWebhookProducer
    {
        public WebhookProducer(IRabbitMQClient client, ILogger<RabbitMQProducer> logger) : base(client, logger)
        { }

        public void Send(WebhookScheduledEvent webhookScheduledEvent)
        {
            Publish(QueueNames.WebhooksQueue, webhookScheduledEvent);
        }
    }
}
