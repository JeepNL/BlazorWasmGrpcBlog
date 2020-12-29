using System;
using BlazorWasmGrpcBlog.Shared.Helpers;

namespace BlazorWasmGrpcBlog.Client.Helpers
{
	// TODO, get culture info from browser via JSInterop.
	// private System.Globalization.CultureInfo cultureInfo { get; set; }
	// cultureInfo = new System.Globalization.CultureInfo("en-US");

	public static class DtUtils
	{
		public static string DbUtc2Local(string utcDate, string format = DtFormats.Dt)
		{
			return DateTime.ParseExact(utcDate, DtFormats.DbUtc, System.Globalization.CultureInfo.InvariantCulture).ToString(format); // Local
		}

		public static string ProtoTs2Utc(Google.Protobuf.WellKnownTypes.Timestamp utcTs, string format = DtFormats.HhMm)
		{
			return utcTs.ToDateTimeOffset().ToString(format) + " (UTC)"; // UTC
		}
	}
}
