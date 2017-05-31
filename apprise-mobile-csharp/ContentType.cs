using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppriseMobile
{
	[JsonConverter(typeof(StringEnumConverter))]
    public enum ContentType
    {
		[EnumMember(Value = "CONTENT_WEB")]
        Web,

		[EnumMember(Value = "CONTENT_PDF")]
		Pdf,

		[EnumMember(Value = "CONTENT_VIDEO")]
		Video,

		[EnumMember(Value = "CONTENT_AUDIO")]
		Audio,

		[EnumMember(Value = "CONTENT_IMAGE")]
		Image,

		[EnumMember(Value = "CONTENT_RICH_TEXT")]
		RichText
    }
}
