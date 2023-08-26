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
	[DataMember] public ushort code;

	/// <summary>
	///     The (hopefully) friendly message detailing as to why the request failed
	/// </summary>
	[DataMember] public string message = null!;
}