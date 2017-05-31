using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppriseMobile
{
	[JsonConverter(typeof(StringEnumConverter))]
    public enum PermissionType
    {
		[EnumMember(Value = "VIEW")]
        View,

		[EnumMember(Value = "EDIT")]
		Edit
    }
}
