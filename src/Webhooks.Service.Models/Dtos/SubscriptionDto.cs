namespace Webhooks.Service.Models.Dtos
{
    public class SubscriptionDto : EntityDto
    {
        public string? Url { get; set; }
        public Guid? EventId { get; set; }
    }
}
