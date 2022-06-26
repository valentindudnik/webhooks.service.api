using System.Runtime.Serialization;

namespace Webhooks.Service.Enums
{
    [Serializable]
    public enum EventType
    {
        [EnumMember(Value = "none")]
        None = 0,
        [EnumMember(Value = "invoice_approved")]
        InvoiceApproved = 1,
        [EnumMember(Value = "order_created")]
        OrderCreated = 2,
        [EnumMember(Value = "webhook_scheduled")]
        WebhookScheduled = 3
    }
}
