namespace Webhooks.Service.Models.Events
{
    public class ApproveInvoiceEvent : DomainEvent
    {
        public Guid InvoiceId { get; set; }
        public int Number { get; set; }
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
    }
}
