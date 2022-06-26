using System.Runtime.Serialization;

namespace Webhooks.Service.Models.Exceptions
{
    [Serializable]
    public class WebhookNotFoundException : ApplicationException
    {
        public WebhookNotFoundException()
        { }

        public WebhookNotFoundException(string message) : base(message)
        { }

        public WebhookNotFoundException(string message, Exception inner) : base(message, inner)
        { }

        public WebhookNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
