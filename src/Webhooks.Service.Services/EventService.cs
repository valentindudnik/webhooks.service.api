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
    public class EventService : IEventService
    {
        private readonly IGenericRepository<Event> _repository;

        private readonly IMapper _mapper;
        private readonly ILogger<EventService> _logger;

        public EventService(IGenericRepository<Event> repository, IMapper mapper, ILogger<EventService> logger)
        {
            _repository = repository;

            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddAsync(EventParameters parameters)
        {
            _logger.LogInformation($"{nameof(AddAsync)} with parameters.");

            var targetEvent = _mapper.Map<EventDto>(parameters);

            var entity = _mapper.Map<Event>(targetEvent);

            await _repository.AddAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Event)} was added successfully.");
        }

        public async Task UpdateAsync(Guid eventId, EventParameters parameters)
        {
            _logger.LogInformation($"{nameof(UpdateAsync)} with Id: {eventId}.");

            var entity = await _repository.GetAsync(x => x.Id == eventId);
            if (entity == null)
            {
                var message = $"{nameof(Event)} with Id: {eventId} not found.";

                _logger.LogError(message);
                throw new EventNotFoundException(message);
            }

            var targetEvent = _mapper.Map<EventDto>(parameters);

            var targetEntity = _mapper.Map<Event>(targetEvent);

            entity.Name = targetEntity.Name;
            entity.EventType = targetEntity.EventType;
            entity.Updated = DateTime.UtcNow;
            entity.IsActive = true;

            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Event)} with Id: {eventId} was updated successfully.");
        }

        public async Task DeleteAsync(Guid eventId)
        {
            _logger.LogInformation($"{nameof(DeleteAsync)} with Id: {eventId}.");

            var entity = await _repository.GetAsync(x => x.Id == eventId && x.IsActive);
            if (entity == null)
            {
                var message = $"{nameof(Event)} with Id: {eventId} not found.";

                _logger.LogError(message);
                throw new EventNotFoundException(message);
            }

            entity.Updated = DateTime.UtcNow;
            entity.IsActive = false;

            await _repository.UpdateAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Event)} with Id: {eventId} was deleted successfully.");
        }

        public async Task<EventDto> GetAsync(Guid eventId)
        {
            _logger.LogInformation($"{nameof(GetAsync)} with Id: {eventId}.");

            var entity = await _repository.GetAsync(x => x.Id == eventId && x.IsActive);
            if (entity == null)
            {
                var message = $"{nameof(Event)} with Id: {eventId} not found.";

                _logger.LogError(message);
                throw new EventNotFoundException(message);
            }

            var result = _mapper.Map<EventDto>(entity);

            _logger.LogInformation($"{nameof(Event)} with Id: {eventId} was getting successfully.");

            return result;
        }

        public async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            _logger.LogInformation($"{nameof(GetAllAsync)}.");

            var entities = await _repository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<EventDto>>(entities);

            _logger.LogInformation($"{nameof(Event)}s were getting successfully.");

            return result;
        }
    }
}
