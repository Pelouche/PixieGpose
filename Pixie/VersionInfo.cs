// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis
{
	using System;

	public static class VersionInfo
	{
		/// <summary>
		/// The time this version was published.
		/// </summary>
		// This is written to by the build server. do not change.
		// However, I am a dumb programmer.
		public static readonly DateTime Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

		/// <summary>
		/// The latest gamve version that the tool has been validated for.
		/// </summary>
		public static readonly string ValidatedGameVersion = "2022.04.07.0000.0000";
	}
}
