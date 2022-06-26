namespace Webhooks.Service.Models.Parameters
{
    public class SubscriptionParameters
    {
        public string? Url { get; set; }
        public Guid? EventId { get; set; }
    }
}
