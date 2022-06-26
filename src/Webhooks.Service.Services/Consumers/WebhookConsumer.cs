using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;
using Webhooks.RabbitMQ.Client.Consumers;
using Webhooks.RabbitMQ.Client.Interfaces;
using Webhooks.RabbitMQ.Models.Common;
using Webhooks.Service.Models.Events;
using Webhooks.Service.Services.Interfaces;
using Webhooks.Service.Services.Interfaces.Consumers;

namespace Webhooks.Service.Services.Consumers
{
    public class WebhookConsumer : RabbitMQConsumer, IWebhookConsumer
    {
        private readonly IServiceProvider _serviceProvider;

        public WebhookConsumer(IRabbitMQClient client, IServiceProvider serviceProvider) : base(client)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async void ReceivedEvent(object sender, BasicDeliverEventArgs eventArgs)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageService = scope.ServiceProvider.GetService<IMessageService>();
                var logger = scope.ServiceProvider.GetService<ILogger<WebhookConsumer>>();

                if (messageService != null && logger != null && eventArgs.RoutingKey == QueueNames.WebhooksQueue)
                {
                    logger.LogInformation($"{nameof(WebhookConsumer)}: ReceivedEvent");

                    var webhookScheduledEvent = JsonConvert.DeserializeObject<WebhookScheduledEvent>(Encoding.UTF8.GetString(eventArgs.Body.Span));
                    if (webhookScheduledEvent != null && webhookScheduledEvent.EventType == Enums.EventType.WebhookScheduled)
                    {
                        await messageService.SendAsync(webhookScheduledEvent);
                    }

                    logger.LogInformation($"{nameof(WebhookConsumer)}: webhook scheduled event was sent successfully.");
                }
            }
        }
    }
}
