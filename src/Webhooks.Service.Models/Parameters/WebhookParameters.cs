using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.Models.Parameters
{
    public class WebhookParameters
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WebhookMethod? WebhookMethod { get; set; }
        public Guid? EventId { get; set; }
    }
}
