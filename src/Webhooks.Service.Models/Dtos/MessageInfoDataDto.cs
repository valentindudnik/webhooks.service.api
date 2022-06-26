using Newtonsoft.Json;

namespace Webhooks.Service.Models.Dtos
{
    public class MessageInfoDataDto
    {
        [JsonProperty("invoiceId")]
        public int InvoiceId { get; set; }
    }
}
