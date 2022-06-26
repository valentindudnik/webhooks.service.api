using System.Runtime.Serialization;

namespace Webhooks.Service.Models.Exceptions
{
    [Serializable]
    public class EventNotFoundException : ApplicationException
    {
        public EventNotFoundException()
        { }

        public EventNotFoundException(string message) : base(message)
        { }

        public EventNotFoundException(string message, Exception inner) : base(message, inner)
        { }

        public EventNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
