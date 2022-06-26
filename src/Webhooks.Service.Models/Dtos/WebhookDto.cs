using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.Models.Dtos
{
    public class WebhookDto : EntityDto
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WebhookMethod WebhookMethod { get; set; }
        public Guid? EventId { get; set; }
    }
}
