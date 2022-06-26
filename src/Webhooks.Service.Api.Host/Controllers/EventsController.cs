using Microsoft.AspNetCore.Mvc;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Models.Result;
using Webhooks.Service.Services.Interfaces;

namespace Webhooks.Service.Api.Host.Controllers
{
    [//Authorize,
     ApiVersion("1.0"),
     Route("api/v{version:apiVersion}/[controller]"),
     ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        /// <summary>
        /// Add event
        /// </summary>
        /// <returns></returns>
        [HttpPost,
         ProducesResponseType(typeof(EntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] EventParameters parameters)
        {
            var result = await _service.AddAsync(parameters);

            return Ok(result);
        }

        /// <summary>
        /// Update event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPut("{eventId}"),
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid eventId, [FromBody] EventParameters parameters)
        {
            await _service.UpdateAsync(eventId, parameters);

            return NoContent();
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}"),
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid eventId)
        {
            await _service.DeleteAsync(eventId);

            return NoContent();
        }

        /// <summary>
        /// Get event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{eventId}"),
         ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid eventId)
        {
            var result = await _service.GetAsync(eventId);

            return Ok(result);
        }

        /// <summary>
        /// Get events
        /// </summary>
        /// <returns></returns>
        [HttpGet,
         ProducesResponseType(typeof(IEnumerable<EventDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }
    }
}
