using System;
using System.Drawing;

namespace Discord.Webhook;

public static class Extensions
{
	/// <summary>
	///     Converts a <see cref="System.Drawing.Color" /> to a <see cref="DColor" /> to be used in an embed
	/// </summary>
	/// <param name="color">The color to convert</param>
	/// <returns>The converted color</returns>
	public static DColor ToDColor(this Color color)
	{
		return new DColor(color.R, color.G, color.B);
	}

	/// <summary>
	///     Validates that a username passes the Discord validation standards
	/// </summary>
	/// <param name="u">The username to validate</param>
	/// <exception cref="ArgumentException">Thrown when the validation fails</exception>
	internal static void ValidateWebhookUsername(this string? u)
	{
		// The reason this is built like this is because it's internal therefore we want it to be simple to implement
		if (u?.ToLower().Contains("discord") == true)
			throw new ArgumentException(
				// ReSharper disable once NotResolvedInText
				"Username cannot contain the word Discord, this is a restriction on Discord's end", "username");
	}
}