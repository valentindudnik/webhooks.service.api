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
    public class WebhooksController : ControllerBase
    {
        private readonly IWebhookService _service;

        public WebhooksController(IWebhookService service)
        {
            _service = service;
        }

        /// <summary>
        /// Add webhook
        /// </summary>
        /// <returns></returns>
        [HttpPost,
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddAsync([FromBody] WebhookParameters paramaeters)
        {
            await _service.AddAsync(paramaeters);

            return NoContent();
        }

        /// <summary>
        /// Update webhook
        /// </summary>
        /// <returns></returns>
        [HttpPut("{webhookId}"),
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid webhookId, [FromBody] WebhookParameters paramaeters)
        {
            await _service.UpdateAsync(webhookId, paramaeters);

            return NoContent();
        }

        /// <summary>
        /// Delete webhook
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{webhookId}"),
         ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid webhookId)
        {
            await _service.DeleteAsync(webhookId);

            return NoContent();
        }

        /// <summary>
        /// Get webhook
        /// </summary>
        /// <param name="webhookId"></param>
        /// <returns></returns>
        [HttpGet("{webhookId}"),
         ProducesResponseType(typeof(WebhookDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid webhookId)
        {
            var result = await _service.GetAsync(webhookId);

            return Ok(result);
        }

        /// <summary>
        /// Get webhooks
        /// </summary>
        /// <returns></returns>
        [HttpGet,
         ProducesResponseType(typeof(IEnumerable<WebhookDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }
    }
}
