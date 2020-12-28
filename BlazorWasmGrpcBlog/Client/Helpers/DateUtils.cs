using System;

namespace BlazorWasmGrpcBlog.Client.Helpers
{
	public static class DateUtils
	{
		public const string DbUtcStrFormat = "yyyy-MM-dd HH:mm:ss";
		public const string DtStrFormat = "yyyy-MM-dd HH:mm";
		public const string HhMmStrFormat = "HH:mm";

		public static string DbUtc2Str(string utcDate, string format = DtStrFormat, bool local = true)
		{
			var ret = (local)
				? DateTime.ParseExact(utcDate, DbUtcStrFormat, System.Globalization.CultureInfo.InvariantCulture).ToLocalTime().ToString(format) + " (Local)"
				: DateTime.ParseExact(utcDate, DbUtcStrFormat, System.Globalization.CultureInfo.InvariantCulture).ToString(format) + " (UTC)"
			;
			return ret;
		}

		public static string ProtoTs2Str(Google.Protobuf.WellKnownTypes.Timestamp utcTs, string format = HhMmStrFormat)
		{
			return utcTs.ToDateTime().ToString(format) + " (UTC)";
		}
	}
}
