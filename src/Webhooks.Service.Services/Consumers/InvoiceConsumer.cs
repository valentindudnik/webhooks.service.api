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
    public class InvoiceConsumer : RabbitMQConsumer, IInvoiceConsumer
    {
        private readonly IServiceProvider _serviceProvider;
        
        public InvoiceConsumer(IRabbitMQClient client, IServiceProvider serviceProvider) : base(client)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async void ReceivedEvent(object sender, BasicDeliverEventArgs eventArgs)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var invoiceService = scope.ServiceProvider.GetService<IInvoiceService>();
                var logger = scope.ServiceProvider.GetService<ILogger<InvoiceConsumer>>();
                
                if (invoiceService != null && logger != null && eventArgs.RoutingKey == QueueNames.InvoicesQueue)
                {
                    logger.LogInformation($"{nameof(InvoiceConsumer)}: ReceivedEvent");

                    var approveInvoiceEvent = JsonConvert.DeserializeObject<ApproveInvoiceEvent>(Encoding.UTF8.GetString(eventArgs.Body.Span));
                    if (approveInvoiceEvent != null && approveInvoiceEvent.EventType == Enums.EventType.InvoiceApproved)
                    {
                        await invoiceService.SendWebhookScheduledEventsAsync(approveInvoiceEvent);
                    }

                    logger.LogInformation($"{nameof(InvoiceConsumer)}: webhook scheduled events were sent successfully.");
                }
            }
        }
    }
}
