using Microsoft.AspNetCore.Mvc;
using Webhooks.Service.Models.Dtos;
using Webhooks.Service.Models.Parameters;
using Webhooks.Service.Services.Interfaces;

namespace Webhooks.Service.Api.Host.Controllers
{
    [//Authorize,
     ApiVersion("1.0"),
     Route("api/v{version:apiVersion}/[controller]"),
     ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _service;

        public SubscriptionsController(ISubscriptionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Add subscription
        /// </summary>
        /// <returns></returns>
        [HttpPost,
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddAsync([FromBody] SubscriptionParameters parameters)
        {
            await _service.AddAsync(parameters);

            return NoContent();
        }

        /// <summary>
        /// Update subscription
        /// </summary>
        /// <returns></returns>
        [HttpPut("{subscriptionId}"),
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid subscriptionId, [FromBody] SubscriptionParameters parameters)
        {
            await _service.UpdateAsync(subscriptionId, parameters);

            return NoContent();
        }

        /// <summary>
        /// Delete subsciprtion
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{subscriptionId}"),
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid subscriptionId)
        {
            await _service.DeleteAsync(subscriptionId);

            return NoContent();
        }

        /// <summary>
        /// Get subscription
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        [HttpGet("{subscriptionId}"),
         ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid subscriptionId)
        {
            var result = await _service.GetAsync(subscriptionId);

            return Ok(result);
        }

        /// <summary>
        /// Get subscriptions
        /// </summary>
        /// <returns></returns>
        [HttpGet,
         ProducesResponseType(typeof(IEnumerable<SubscriptionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }
    }
}
