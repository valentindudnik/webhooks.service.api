using System.Runtime.Serialization;

namespace Webhooks.Service.Enums
{
    [Serializable]
    public enum WebhookMethod
    {
        [EnumMember(Value = "NONE")]
        None = 0,
        [EnumMember(Value = "POST")]
        Post = 1,
        [EnumMember(Value = "PUT")]
        Put = 2,
        [EnumMember(Value = "DELETE")]
        Delete = 3,
        [EnumMember(Value = "GET")]
        Get = 4,
        [EnumMember(Value = "PATCH")]
        Patch = 5,
        [EnumMember(Value = "OPTIONS")]
        Options = 6
    }
}
