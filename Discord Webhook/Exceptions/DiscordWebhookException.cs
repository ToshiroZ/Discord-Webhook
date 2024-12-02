using System;
using Discord.Webhook.Models;

namespace Discord.Webhook.Exceptions;

/// <summary>
///     Thrown when webhook execution fails, the message contains the actual error thrown by Discord
/// </summary>
public class DiscordWebhookException : Exception
{
	/// <summary>
	///     Constructs a <see cref="DiscordWebhookException" />
	/// </summary>
	/// <param name="error">The error thrown by Discord</param>
	internal DiscordWebhookException(DiscordError error) : base(error.Message)
	{
	}
	
	/// <summary>
	///     Constructs a <see cref="DiscordWebhookException" />
	/// </summary>
	/// <param name="message">The fallback string message</param>
	internal DiscordWebhookException(string message) : base(message)
	{
	}
}