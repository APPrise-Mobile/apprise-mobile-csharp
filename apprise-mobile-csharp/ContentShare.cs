using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppriseMobile
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ContentShare
	{
		[EnumMember(Value = "SHARE_DISABLED")]
		Disabled,
		[EnumMember(Value = "SHARE_EMAIL_ONLY")]
		EmailOnly,
		[EnumMember(Value = "SHARE_ENABLED")]
		Enabled
	}
}
