using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.RabbitMQ.Models.Events;
using Webhooks.Service.Enums;

namespace Webhooks.Service.Models.Events
{
    public class DomainEvent : BaseEvent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType EventType { get; set; }
    }
}
