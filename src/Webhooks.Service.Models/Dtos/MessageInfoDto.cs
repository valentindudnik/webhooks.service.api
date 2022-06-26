using Newtonsoft.Json;

namespace Webhooks.Service.Models.Dtos
{
    public class MessageInfoDto
    {
        [JsonProperty("event_name")]
        public string? EventName { get; set; }
        [JsonProperty("data")]
        public MessageInfoDataDto? Data { get; set; }
    }
}
