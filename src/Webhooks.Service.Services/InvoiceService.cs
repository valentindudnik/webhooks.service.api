using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Webhooks.Service.DataAccess.Interfaces;
using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Configurations;
using Webhooks.Service.Models.Events;
using Webhooks.Service.Services.Interfaces;
using Webhooks.Service.Services.Interfaces.Producers;

namespace Webhooks.Service.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IGenericRepository<Event> _eventsRepository;
        private readonly IGenericRepository<Webhook> _webhooksRepository;
        private readonly IGenericRepository<Subscription> _subscriptionsRepository;

        private readonly IMessageService _messageService;

        private readonly IWebhookProducer _webhookProducer;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly FeaturesConfiguration _featuresConfiguration;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(IGenericRepository<Event> eventsRepository, 
                              IGenericRepository<Webhook> webhooksRepository,
                              IGenericRepository<Subscription> subscriptionsRepository,
                              IHttpClientFactory httpClientFactory,
                              IWebhookProducer webhookProducer, 
                              IMessageService messageService,
                              IOptions<FeaturesConfiguration> featuresConfigurationOptions,
                              ILogger<InvoiceService> logger)
        {
            _eventsRepository = eventsRepository;
            _webhooksRepository = webhooksRepository;
            _subscriptionsRepository = subscriptionsRepository;

            _messageService = messageService;
            _webhookProducer = webhookProducer;
            _httpClientFactory = httpClientFactory;

            _featuresConfiguration = featuresConfigurationOptions.Value;
            _logger = logger;
        }

        public async Task SendWebhookScheduledEventsAsync(ApproveInvoiceEvent approveInvoiceEvent)
        {
            _logger.LogInformation($"{nameof(SendWebhookScheduledEventsAsync)} with {nameof(approveInvoiceEvent.Id)}: {approveInvoiceEvent.Id}, " +
                                                                                  $"{nameof(approveInvoiceEvent.InvoiceId)}: {approveInvoiceEvent.InvoiceId}.");

            var tasks = new List<Task<HttpResponseMessage>>();

            using (var client = _httpClientFactory.CreateClient())
            {
                var events = _eventsRepository.Where(x => x.EventType == approveInvoiceEvent.EventType && x.IsActive).ToArray();

                foreach (var eventItem in events)
                {
                    var webhooks = _webhooksRepository.Where(x => x.IsActive && x.EventId == eventItem.Id).ToArray();
                    var subscriptions = _subscriptionsRepository.Where(x => x.IsActive && x.EventId == eventItem.Id).ToArray();

                    foreach (var webhook in webhooks)
                    {
                        foreach (var subscription in subscriptions)
                        {
                            var webhookScheduledEvent = new WebhookScheduledEvent
                            {
                                Created = DateTime.UtcNow,
                                Id = Guid.NewGuid(),
                                EventType = Enums.EventType.WebhookScheduled,

                                // webhook
                                WebhookId = webhook.Id,
                                WebhookMethod = webhook.WebhookMethod,

                                // subscription
                                SubscriptionId = subscription.Id,
                                SubscrptionUrl = subscription.Url,

                                // invoice
                                Currency = approveInvoiceEvent.Currency,
                                Date = approveInvoiceEvent.Date,
                                Description = approveInvoiceEvent.Description,
                                Discount = approveInvoiceEvent.Discount,
                                DueDate = approveInvoiceEvent.DueDate,
                                EventId = eventItem.Id,
                                EventName = eventItem.Name,
                                HasApproved = approveInvoiceEvent.HasApproved,
                                InvoiceFrom = approveInvoiceEvent.InvoiceFrom,
                                InvoiceTo = approveInvoiceEvent.InvoiceTo,
                                InvoiceId = approveInvoiceEvent.InvoiceId,
                                InvoiceNumber = approveInvoiceEvent.Number,
                                Price = approveInvoiceEvent.Price,
                                Quantity = approveInvoiceEvent.Quantity,
                                Tax = approveInvoiceEvent.Tax,
                                Total = approveInvoiceEvent.Total
                            };

                            if (_featuresConfiguration.WebhookQueueFeatureEnabled)
                            {
                                _webhookProducer.Send(webhookScheduledEvent);
                            }
                            else
                            {
                                tasks.Add(client.SendAsync(_messageService.CreateRequest(webhookScheduledEvent), CancellationToken.None));
                            }
                        }
                    }
                }

                if (!_featuresConfiguration.WebhookQueueFeatureEnabled)
                {
                    try
                    {
                        await Task.WhenAll(tasks.ToArray());
                    }
                    catch (Exception exc)
                    {
                        _logger.LogError(exc, exc.Message);
                    }

                    var messages = new List<Message>();

                    foreach (var task in tasks)
                    {
                        if (!task.IsFaulted)
                        {
                            var response = await task;
                            var requestContent = await response?.RequestMessage?.Content?.ReadAsStringAsync()!;
                            var responseContent = await response.Content.ReadAsStringAsync();

                            var message = new Message
                            {
                                Id = Guid.NewGuid(),
                                Url = response.RequestMessage?.RequestUri?.ToString(),
                                StatusCode = (int)response.StatusCode,
                                RequestContent = requestContent,
                                ResponseContent = responseContent,
                                MessageStatus = response.IsSuccessStatusCode ? Enums.MessageStatus.Successed : Enums.MessageStatus.Failure,
                                Created = DateTime.UtcNow,
                                Updated = DateTime.UtcNow,
                                IsActive = true
                            };
                            messages.Add(message);
                        }
                        else if (task.IsFaulted || task.IsCanceled)
                        {
                            var message = new Message
                            {
                                Id = Guid.NewGuid(),
                                Url = task.Exception?.Message,
                                StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                                RequestContent = task.Exception?.Message,
                                ResponseContent = task.Exception?.Message,
                                MessageStatus = Enums.MessageStatus.Failure,
                                Created = DateTime.UtcNow,
                                Updated = DateTime.UtcNow,
                                IsActive = true
                            };
                            messages.Add(message);
                        }
                    }

                    if (messages.Count > 0)
                    {
                        await _messageService.AddAsync(messages);
                    }
                }
            }

            _logger.LogInformation($"{nameof(SendWebhookScheduledEventsAsync)}: events were sent successfully.");
        }
    }
}
