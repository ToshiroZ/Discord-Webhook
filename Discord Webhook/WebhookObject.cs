using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

// ReSharper disable InconsistentNaming

namespace Discord.Webhook;

/// <summary>
///     The webhook message object, allows you to set plaintext messages, images or embeds
/// </summary>
[DataContract]
public class WebhookObject
{
	/// <summary>
	///     Just a basic constructor
	/// </summary>
	public WebhookObject()
	{
		embeds = new List<Embed>();
		content = null;
	}

	/// <summary>
	///     The message of the embed
	/// </summary>
	[DataMember] public string? content;

	/// <summary>
	///     the username you shouldn't be able to see this
	/// </summary>
	[DataMember] internal string? username;

	/// <summary>
	///     the avatar_url you shouldn't be able to see this
	/// </summary>
	[DataMember] internal string? avatar_url;

	/// <summary>
	///     The name of thread to create (requires the webhook channel to be a forum channel)
	/// </summary>
	[DataMember] internal string? thread_name;

	/// <summary>
	///     will the content be read out
	/// </summary>
	[DataMember] public bool tts;

	/// <summary>
	///     the embeds to send with the webhook there is a max of 25
	/// </summary>
	[DataMember] public List<Embed> embeds;

	/// <summary>
	///     converts the <see cref="WebhookObject" /> to JSON for posting to the URL
	/// </summary>
	/// <returns>The JSON string</returns>
	public override string ToString()
	{
		// Set the content, ensure it is never null
		content ??= string.Empty;

		// Check if more than 25 embeds have been added
		if (embeds.Count > 25) throw new Exception("You can only have a maximum of 25 embeds in a message");

		// Check if any of the embeds have more than 25 fields
		if (embeds.Any(x => x.fields.Count > 25))
			throw new Exception("You can only have a maximum of 25 fields in an embed");

		// Check if the message has a content length of more than 1024 characters
		if (content.Length > 1024)
			throw new Exception("You can only have a maximum of 1024 characters in a message");

		// Check if any of the embeds have a description or any fields that are longer than 1024 characters
		if (embeds.Any(x => x.description?.Length > 1024 || x.fields.Any(y => y.value.ToString().Length > 1024)))
			throw new Exception("You can only have a maximum of 1024 characters in a message");

		// Loop through and set the colors, default value being black
		foreach (var embed in embeds) embed.color = (int)embed.Color.RawValue;

		// Serialize the object to JSON and return
		return JsonSerializer<WebhookObject>.Serialize(this);
	}

	/// <summary>
	///     Adds an embed using the <see cref="EmbedBuilder" />
	/// </summary>
	/// <param name="builder">The builder to use when adding the embed</param>
	/// <returns>The updated <see cref="WebhookObject" /> with the new embed</returns>
	public WebhookObject AddEmbed(EmbedBuilder builder)
	{
		embeds.Add(builder.Build());
		return this;
	}

	/// <summary>
	///     Adds an embed
	/// </summary>
	/// <param name="embed">The embed to add to the collection</param>
	/// <returns>The updated <see cref="WebhookObject" /> with the new embed</returns>
	public WebhookObject AddEmbed(Embed embed)
	{
		embeds.Add(embed);
		return this;
	}

	/// <summary>
	///     Adds an embed using the <see cref="EmbedBuilder" /> with an <see cref="Action" />
	/// </summary>
	/// <param name="builder">The builder to use when adding the embed</param>
	public WebhookObject AddEmbed(Action<EmbedBuilder> builder)
	{
		var embedBuilder = new EmbedBuilder();
		builder(embedBuilder);
		AddEmbed(embedBuilder);

		return this;
	}
}

/// <summary>
///     the embed class
/// </summary>
[DataContract]
public class Embed
{
	/// <summary>
	///     the title of the embed
	/// </summary>
	[DataMember] public string? title;

	/// <summary>
	///     the type of embed
	/// </summary>
	[DataMember] public string? type;

	/// <summary>
	///     the message in the embed
	/// </summary>
	[DataMember] public string? description;

	/// <summary>
	///     the url the title links to
	/// </summary>
	[DataMember] public string? url;

	/// <summary>
	///     the timestamp of the embed
	/// </summary>
	[DataMember] public string? timestamp;

	/// <summary>
	///     the color of the embed
	/// </summary>
	public DColor Color = Colors.Black;

	/// <summary>
	///     the discord int color of the color you shouldnt see this
	/// </summary>
	[DataMember] internal int color;

	/// <summary>
	///     the footer of the embed
	/// </summary>
	[DataMember] public Footer? footer;

	/// <summary>
	///     the image the embed will have
	/// </summary>
	[DataMember] public Image? image;

	/// <summary>
	///     the thumbnail of the embed
	/// </summary>
	[DataMember] public Thumbnail? thumbnail;

	/// <summary>
	///     any videos the embed will play
	/// </summary>
	[DataMember] public Video? video;

	/// <summary>
	///     the provider of the embed
	/// </summary>
	[DataMember] public Provider? provider;

	/// <summary>
	///     the author of the embed
	/// </summary>
	[DataMember] public Author? author;

	/// <summary>
	///     the fields the embed can have capped at 25
	/// </summary>
	[DataMember] public List<Field> fields = new();
}

[DataContract]
public class Field
{
	/// <summary>
	///     the title of the field
	/// </summary>
	[DataMember] public string name = null!;

	/// <summary>
	///     the content of the field
	/// </summary>
	[DataMember] public object value = null!;

	/// <summary>
	///     whether the embed is inline
	/// </summary>
	[DataMember] public bool inline;
}

[DataContract]
public class Footer
{
	/// <summary>
	///     the footer text
	/// </summary>
	[DataMember] public string text = null!;

	/// <summary>
	///     the url the icon will have
	/// </summary>
	[DataMember] public string? icon_url;

	/// <summary>
	///     the proxy_icon_url ima be honest no clue but its in the docs so is here
	/// </summary>
	[DataMember] public string? proxy_icon_url;
}

[DataContract]
public class Image
{
	/// <summary>
	///     the url of the image
	/// </summary>
	[DataMember] public string url = null!;

	/// <summary>
	///     the proxy_url to the image dont ask me its in the docs (a proxied url of the image)
	/// </summary>
	[DataMember] public string? proxy_url;

	/// <summary>
	///     the height in pixels the image will be
	/// </summary>
	[DataMember] public int? height;

	/// <summary>
	///     the width in pixels the image will be
	/// </summary>
	[DataMember] public int? width;
}

[DataContract]
public class Thumbnail
{
	/// <summary>
	///     the url to the image
	/// </summary>
	[DataMember] public string url = null!;

	/// <summary>
	///     in the docs no idea
	/// </summary>
	[DataMember] public string? proxy_url;

	/// <summary>
	///     the height in pixels the image will be
	/// </summary>
	[DataMember] public int? height;

	/// <summary>
	///     the width in pixels the image will be
	/// </summary>
	[DataMember] public int? width;
}

[DataContract]
public class Video
{
	/// <summary>
	///     the url to the video
	/// </summary>
	[DataMember] public string? url;
	
	/// <summary>
	///     in the docs no idea
	/// </summary>
	[DataMember] public string? proxy_url;

	/// <summary>
	///     the height in pixels the videp will be
	/// </summary>
	[DataMember] public int? height;

	/// <summary>
	///     the width in pixels the video will be
	/// </summary>
	[DataMember] public int? width;
}

[DataContract]
public class Provider
{
	/// <summary>
	///     the provider name
	/// </summary>
	[DataMember] public string? name;

	/// <summary>
	///     the url to the provider
	/// </summary>
	[DataMember] public string? url;
}

[DataContract]
public class Author
{
	/// <summary>
	///     the authors name
	/// </summary>
	[DataMember] public string name = null!;

	/// <summary>
	///     the url the author should link to
	/// </summary>
	[DataMember] public string? url;

	/// <summary>
	///     the authors icon
	/// </summary>
	[DataMember] public string? icon_url;

	[DataMember] public string? proxy_icon_url;
}

public class DColor
{
	internal uint RawValue { get; }

	/// <summary>
	///     Creates a new color to send to discord
	///     The reason it is not System.Drawing.Color is to ensure compat with the color in the media and to support more
	///     people heading forward
	/// </summary>
	/// <param name="r">The 8-bit integer representing the red value</param>
	/// <param name="g">The 8-bit integer representing the green value</param>
	/// <param name="b">The 8-bit integer representing the blue value</param>
	public DColor(int r, int g, int b)
	{
		// Validation to ensure the arguments are valid ints
		if (r is < 0 or > 255) throw new ArgumentOutOfRangeException(nameof(r), "Value must be within [0,255].");
		if (g is < 0 or > 255) throw new ArgumentOutOfRangeException(nameof(g), "Value must be within [0,255].");
		if (b is < 0 or > 255) throw new ArgumentOutOfRangeException(nameof(b), "Value must be within [0,255].");

		RawValue =
			((uint)r << 16) |
			((uint)g << 8) |
			(uint)b;
	}
	
	/// <summary>
	///     Creates a new color to send to discord
	///     The reason it is not System.Drawing.Color is to ensure compat with the color in the media and to support more
	///     people heading forward
	/// </summary>
	/// <param name="r">The 8-bit integer representing the red value</param>
	/// <param name="g">The 8-bit integer representing the green value</param>
	/// <param name="b">The 8-bit integer representing the blue value</param>
	public DColor(byte r, byte g, byte b)
	{
		RawValue =
			((uint)r << 16) |
			((uint)g << 8) |
			b;
	}
}