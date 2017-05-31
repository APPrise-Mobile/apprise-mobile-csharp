using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppriseMobile
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ManageType
    {
        [EnumMember(Value = "ACCOUNT")]
        Account,

        [EnumMember(Value = "APP_PROFILE")]
        AppProfile
    }
}
