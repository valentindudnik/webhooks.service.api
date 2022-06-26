using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.DataAccess.Models.Entities
{
    public class Webhook : Entity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WebhookMethod WebhookMethod { get; set; }
        public Guid? EventId { get; set; }
    }
}
