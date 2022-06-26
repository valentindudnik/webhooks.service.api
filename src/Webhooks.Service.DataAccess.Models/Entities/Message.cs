using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.DataAccess.Models.Entities
{
    public class Message : Entity
    {
        public string? Url { get; set; }
        public string? RequestContent { get; set; }
        public string? ResponseContent { get; set; }
        public int StatusCode { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageStatus MessageStatus { get; set; }
    }
}
