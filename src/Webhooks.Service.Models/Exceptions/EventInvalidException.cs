using System.Runtime.Serialization;

namespace Webhooks.Service.Models.Exceptions
{
    [Serializable]
    public class EventInvalidException : ApplicationException
    {
        public EventInvalidException()
        { }

        public EventInvalidException(string message) : base(message)
        { }

        public EventInvalidException(string message, Exception inner) : base(message, inner)
        { }

        public EventInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
