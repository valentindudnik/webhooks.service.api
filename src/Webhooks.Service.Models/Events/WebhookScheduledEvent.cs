using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Webhooks.Service.Enums;

namespace Webhooks.Service.Models.Events
{
    public class WebhookScheduledEvent : DomainEvent
    {
        public Guid SubscriptionId { get; set; }
        public string? SubscrptionUrl { get; set; }

        public Guid InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public int Quantity { get; set; }
        public string? InvoiceTo { get; set; }
        public string? InvoiceFrom { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public bool HasApproved { get; set; }

        public Guid EventId { get; set; }
        public string? EventName { get; set; }

        public Guid WebhookId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WebhookMethod WebhookMethod { get; set; }
    }
}
