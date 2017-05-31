using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppriseMobile
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobStatus
    {
        [EnumMember(Value = "NONE")]
        None,

        [EnumMember(Value = "QUEUED")]
        Queued,

        [EnumMember(Value = "PROCESSING")]
        Processing,

        [EnumMember(Value = "COMPLETED")]
        Completed,

        [EnumMember(Value = "FAILED")]
        Failed
    }
}
