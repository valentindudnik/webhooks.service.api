using System.Runtime.Serialization;

namespace Webhooks.Service.Models.Exceptions
{
    [Serializable]
    public class SubscriptionNotFoundException : ApplicationException
    {
        public SubscriptionNotFoundException()
        { }

        public SubscriptionNotFoundException(string message) : base(message)
        { }

        public SubscriptionNotFoundException(string message, Exception inner) : base(message, inner)
        { }

        public SubscriptionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
