using Webhooks.Service.Models.Events;

namespace Webhooks.Service.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task SendWebhookScheduledEventsAsync(ApproveInvoiceEvent approveInvoiceEvent);
    }
}
