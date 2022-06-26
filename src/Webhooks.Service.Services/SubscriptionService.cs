using AutoMapper;
using Microsoft.Extensions.Logging;
using Webhooks.Service.DataAccess.Interfaces;
using Webhooks.Service.DataAccess.Models.Entities;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Exceptions;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Models.Result;
using Webhooks.Service.Services.Interfaces;

namespace Webhooks.Service.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IGenericRepository<Subscription> _repository;
        
        private readonly IMapper _mapper;
        private readonly ILogger<SubscriptionService> _logger;
        
        public SubscriptionService(IGenericRepository<Subscription> repository, IMapper mapper, ILogger<SubscriptionService> logger)
        {
            _repository = repository;

            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EntityResult> AddAsync(SubscriptionParameters parameters)
        {
            _logger.LogInformation($"{nameof(AddAsync)} with parameters.");

            var targetSubscription = _mapper.Map<SubscriptionDto>(parameters);

            var subscription = _mapper.Map<Subscription>(targetSubscription);

            await _repository.AddAsync(subscription);

            await _repository.SaveChangesAsync();

            var result = _mapper.Map<EntityResult>(subscription);

            _logger.LogInformation($"{nameof(Subscription)} was added successfully.");

            return result;
        }

        public async Task UpdateAsync(Guid subscriptionId, SubscriptionParameters parameters)
        {
            _logger.LogInformation($"{nameof(UpdateAsync)} with Id: {subscriptionId}.");

            var entity = await _repository.GetAsync(x => x.Id == subscriptionId);
            if (entity == null)
            {
                var message = $"{nameof(Subscription)} with Id: {subscriptionId} not found.";

                _logger.LogError(message);
                throw new SubscriptionNotFoundException(message);
            }

            var targetSubscription = _mapper.Map<SubscriptionDto>(parameters);

            var subscription = _mapper.Map<Subscription>(targetSubscription);

            entity.EventId = subscription.EventId;
            entity.Url = subscription.Url;
            entity.Updated = DateTime.UtcNow;
            entity.IsActive = true;

            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Subscription)} with Id: {subscriptionId} was updated successfully.");
        }

        public async Task DeleteAsync(Guid subscriptionId)
        {
            _logger.LogInformation($"{nameof(DeleteAsync)} with Id: {subscriptionId}.");

            var entity = await _repository.GetAsync(x => x.Id == subscriptionId && x.IsActive);
            if (entity == null)
            {
                var message = $"{nameof(Subscription)} with Id: {subscriptionId} not found.";

                _logger.LogError(message);
                throw new SubscriptionNotFoundException(message);
            }

            entity.Updated = DateTime.UtcNow;
            entity.IsActive = false;

            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Subscription)} with Id: {subscriptionId} was deleted successfully.");
        }

        public async Task<SubscriptionDto> GetAsync(Guid subscriptionId)
        {
            _logger.LogInformation($"{nameof(GetAsync)} with Id: {subscriptionId}.");

            var entity = await _repository.GetAsync(x => x.Id == subscriptionId && x.IsActive);
            if (entity == null)
            {
                var message = $"{nameof(Subscription)} with Id: {subscriptionId} not found.";

                _logger.LogError(message);
                throw new SubscriptionNotFoundException(message);
            }

            var result = _mapper.Map<SubscriptionDto>(entity);

            _logger.LogInformation($"{nameof(Subscription)} with Id: {subscriptionId} was getting successfully.");

            return result;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllAsync()
        {
            _logger.LogInformation($"{nameof(GetAllAsync)}.");

            var entities = await _repository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<SubscriptionDto>>(entities);

            _logger.LogInformation($"{nameof(Subscription)}s were getting successfully.");

            return result;
        }
    }
}
