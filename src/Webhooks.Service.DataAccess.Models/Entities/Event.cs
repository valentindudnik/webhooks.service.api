using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.DataAccess.Models.Entities
{
    public class Event : Entity
    {
        public string? Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType EventType { get; set; }
    }
}
