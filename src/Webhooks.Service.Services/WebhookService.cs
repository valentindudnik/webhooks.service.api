using AutoMapper;
using Microsoft.Extensions.Logging;
using Webhooks.Service.DataAccess.Interfaces;
using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Exceptions;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Services.Interfaces;

namespace Webhooks.Service.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly IGenericRepository<Webhook> _repository;

        private readonly IMapper _mapper;
        private readonly ILogger<WebhookService> _logger;

        public WebhookService(IGenericRepository<Webhook> repository, IMapper mapper, ILogger<WebhookService> logger)
        {
            _repository = repository;
         
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddAsync(WebhookParameters parameters)
        {
            _logger.LogInformation($"{nameof(AddAsync)} with parameters.");

            var targetWebhook = _mapper.Map<WebhookDto>(parameters);

            var webhook = _mapper.Map<Webhook>(targetWebhook);

            await _repository.AddAsync(webhook);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Webhook)} was added successfully.");
        }

        public async Task UpdateAsync(Guid webhookId, WebhookParameters parameters)
        {
            _logger.LogInformation($"{nameof(UpdateAsync)} with Id: {webhookId}.");

            var entity = await _repository.GetAsync(x => x.Id == webhookId);
            if (entity == null)
            {
                var message = $"{nameof(Webhook)} with Id: {webhookId} not found.";

                _logger.LogError(message);
                throw new WebhookNotFoundException(message);
            }

            var targetWebhook = _mapper.Map<WebhookDto>(parameters);

            var webhook = _mapper.Map<Webhook>(targetWebhook);

            entity.WebhookMethod = webhook.WebhookMethod;
            entity.EventId = webhook.EventId;
            entity.Updated = DateTime.UtcNow;
            entity.IsActive = true;

            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Webhook)} with Id: {webhookId} was updated successfully.");
        }

        public async Task DeleteAsync(Guid webhookId)
        {
            _logger.LogInformation($"{nameof(DeleteAsync)} with Id: {webhookId}.");

            var entity = await _repository.GetAsync(x => x.Id == webhookId && x.IsActive);
            if (entity == null)
            {
                var message = $"{nameof(Webhook)} with Id: {webhookId} not found.";

                _logger.LogError(message);
                throw new WebhookNotFoundException(message);
            }

            entity.Updated = DateTime.UtcNow;
            entity.IsActive = false;

            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Webhook)} with Id: {webhookId} was deleted successfully.");
        }

        public async Task<WebhookDto> GetAsync(Guid webhookId)
        {
            _logger.LogInformation($"{nameof(DeleteAsync)} with Id: {webhookId}.");

            var entity = await _repository.GetAsync(x => x.Id == webhookId && x.IsActive);
            if (entity == null)
            {
                var message = $"{nameof(Webhook)} with Id: {webhookId} not found.";

                _logger.LogError(message);
                throw new WebhookNotFoundException(message);
            }

            var result = _mapper.Map<WebhookDto>(entity);

            _logger.LogInformation($"{nameof(Webhook)} with Id: {webhookId} was getting successfully.");

            return result;
        }

        public async Task<IEnumerable<WebhookDto>> GetAllAsync()
        {
            _logger.LogInformation($"{nameof(GetAllAsync)}.");

            var entities = await _repository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<WebhookDto>>(entities);

            _logger.LogInformation($"{nameof(Webhook)}s were getting successfully.");

            return result;
        }
    }
}
