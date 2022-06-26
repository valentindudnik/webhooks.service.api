using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.Models.Dtos
{
    public class EventDto : EntityDto
    {
        public string? Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType EventType { get; set; }
    }
}
