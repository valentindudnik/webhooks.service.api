using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Webhooks.Service.DataAccess.Interfaces;
using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Events;
using Webhooks.Service.Services.Interfaces;

namespace Webhooks.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IGenericRepository<Message> _messageRepository;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<MessageService> _logger;

        public MessageService(IHttpClientFactory httpClientFactory, IGenericRepository<Message> messageRepository, ILogger<MessageService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _messageRepository = messageRepository;

            _logger = logger;
        }

        public async Task AddAsync(IEnumerable<Message> messages)
        {
            _logger.LogInformation($"{nameof(AddAsync)} with messages.");

            await _messageRepository.AddAsync(messages);

            await _messageRepository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(AddAsync)} with messages were sent successfully.");
        }

        public async Task SendAsync(WebhookScheduledEvent webhookScheduledEvent)
        {
            _logger.LogInformation($"{nameof(SendAsync)} with {nameof(webhookScheduledEvent.Id)}: {webhookScheduledEvent.Id}");

            var message = new Message
            {
                Id = Guid.NewGuid(),
                Url = webhookScheduledEvent.SubscrptionUrl,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                IsActive = true,
                MessageStatus = Enums.MessageStatus.Pending
            };

            try
            {
                await _messageRepository.AddAsync(message);
                
                using var client = _httpClientFactory.CreateClient();

                var request = CreateRequest(webhookScheduledEvent);
                message.RequestContent = await request?.Content?.ReadAsStringAsync()!;

                var response = await client.SendAsync(request, CancellationToken.None);

                message.StatusCode = (int)response.StatusCode;
                message.ResponseContent = await response.Content.ReadAsStringAsync();
                message.MessageStatus = response.IsSuccessStatusCode ? Enums.MessageStatus.Successed : Enums.MessageStatus.Failure;

                await _messageRepository.UpdateAsync(message);

                _logger.LogInformation($"{nameof(SendAsync)} with {nameof(webhookScheduledEvent.Id)}: {webhookScheduledEvent.Id} was sent successfully.");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);

                message.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                message.ResponseContent = exc.Message;
                message.MessageStatus = Enums.MessageStatus.Failure;

                await _messageRepository.UpdateAsync(message);
            }

            _logger.LogInformation($"{nameof(SendAsync)} with {nameof(webhookScheduledEvent.Id)}: {webhookScheduledEvent.Id} was sent successfully.");
        }

        public HttpRequestMessage CreateRequest(WebhookScheduledEvent webhookScheduledEvent)
        {
            _logger.LogInformation($"{nameof(CreateRequest)} with {nameof(webhookScheduledEvent.Id)}: {webhookScheduledEvent.Id}.");

            HttpRequestMessage? result = null;

            if (webhookScheduledEvent.WebhookMethod == Enums.WebhookMethod.Post)
            {
                result = CreateBodyRequest(HttpMethod.Post, webhookScheduledEvent);
            }
            else if (webhookScheduledEvent.WebhookMethod == Enums.WebhookMethod.Put)
            {
                result = CreateBodyRequest(HttpMethod.Put, webhookScheduledEvent);
            }
            else if (webhookScheduledEvent.WebhookMethod == Enums.WebhookMethod.Patch)
            {
                result = CreateBodyRequest(HttpMethod.Patch, webhookScheduledEvent);
            }
            else if (webhookScheduledEvent.WebhookMethod == Enums.WebhookMethod.Get)
            {
                result = CreateRequest(HttpMethod.Get, webhookScheduledEvent.SubscrptionUrl!);
            }
            else if (webhookScheduledEvent.WebhookMethod == Enums.WebhookMethod.Delete)
            {
                result = CreateRequest(HttpMethod.Delete, webhookScheduledEvent.SubscrptionUrl!);
            }
            else if (webhookScheduledEvent.WebhookMethod == Enums.WebhookMethod.Options)
            {
                result = CreateRequest(HttpMethod.Options, webhookScheduledEvent.SubscrptionUrl!);
            }

            _logger.LogInformation($"{nameof(CreateRequest)} with {nameof(webhookScheduledEvent.Id)}: {webhookScheduledEvent.Id} was created successfully.");

            return result!;
        }

        private HttpRequestMessage CreateBodyRequest(HttpMethod httpMethod, WebhookScheduledEvent webhookScheduledEvent)
        {
            var result = new HttpRequestMessage(httpMethod, webhookScheduledEvent.SubscrptionUrl)
            {
                Content = JsonContent.Create(new MessageInfoDto
                {
                    Data = new MessageInfoDataDto
                    {
                        InvoiceId = webhookScheduledEvent.InvoiceNumber
                    },
                    EventName = webhookScheduledEvent.EventName
                })
            };

            return result;
        }

        private HttpRequestMessage CreateRequest(HttpMethod httpMethod, string subscrptionUrl)
        {
            var result = new HttpRequestMessage(httpMethod, subscrptionUrl);
            
            return result;
        }
    }
}
