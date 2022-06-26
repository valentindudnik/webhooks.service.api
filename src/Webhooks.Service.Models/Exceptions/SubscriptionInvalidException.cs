using System.Runtime.Serialization;

namespace Webhooks.Service.Models.Exceptions
{
    [Serializable]
    public class SubscriptionInvalidException : ApplicationException
    {
        public SubscriptionInvalidException()
        { }

        public SubscriptionInvalidException(string message) : base(message)
        { }

        public SubscriptionInvalidException(string message, Exception inner) : base(message, inner)
        { }

        public SubscriptionInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
