namespace Webhooks.Service.DataAccess.Models.Entities
{
    public class Subscription : Entity
    {
        public Guid? EventId { get; set; }
        public string? Url { get; set; }
    }
}
