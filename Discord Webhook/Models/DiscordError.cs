using System.Runtime.Serialization;

namespace Discord.Webhook.Models;

/// <summary>
///     Represents an error thrown by Discord
/// </summary>
public class DiscordError
{
	/// <summary>
	///     The error code thrown by Discord
	/// </summary>
	[DataMember(Name = "code")] public uint Code;

	/// <summary>
	///     The (hopefully) friendly message detailing as to why the request failed
	/// </summary>
	[DataMember(Name = "message")] public string Message = null!;
}