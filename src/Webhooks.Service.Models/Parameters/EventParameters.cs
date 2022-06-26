using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.Models.Parameters
{
    public class EventParameters
    {
        public string? Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType? EventType { get; set; }
    }
}
