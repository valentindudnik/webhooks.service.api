using Microsoft.Extensions.DependencyInjection;
using Webhooks.Service.Services;
using Webhooks.Service.Services.Interfaces;

namespace Webhooks.Service.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IWebhookService, WebhookService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
