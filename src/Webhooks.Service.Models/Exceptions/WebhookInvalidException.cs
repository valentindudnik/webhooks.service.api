using System.Runtime.Serialization;

namespace Webhooks.Service.Models.Exceptions
{
    [Serializable]
    public class WebhookInvalidException : ApplicationException
    {
        public WebhookInvalidException()
        { }

        public WebhookInvalidException(string message) : base(message)
        { }

        public WebhookInvalidException(string message, Exception inner) : base(message, inner)
        { }

        public WebhookInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
