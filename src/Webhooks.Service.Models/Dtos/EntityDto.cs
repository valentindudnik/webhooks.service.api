namespace Webhooks.Service.Models.Dtos
{
    public class EntityDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsActive { get; set; }
    }
}
