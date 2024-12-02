using System;
using System.Collections.Generic;

namespace Discord.Webhook;

/// <summary>
///     The embed builder class, this allows easy tooling to build embeds using the Fluent design pattern
/// </summary>
public class EmbedBuilder
{
	private string? _title;
	private string? _description;
	private string? _url;
	private DateTime? _timestamp;
	private Image? _image;
	private Thumbnail? _thumbnail;
	private readonly List<Field> _fields = [];
	private Footer? _footer;
	private DColor? _color = Colors.Black;
	private Video? _video;
	private Author? _author;
	private Provider? _provider;

	/// <summary>
	///     Adds a field to the embed
	/// </summary>
	/// <param name="name">The title of the field</param>
	/// <param name="value">The content in the field</param>
	/// <param name="inline">Whether to inline the field</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder AddField(string name, object value, bool inline = false)
	{
		_fields.Add(new Field
		{
			Name = name,
			Value = value,
			IsInline = inline
		});

		return this;
	}

	/// <summary>
	///     Sets color of the embed
	/// </summary>
	/// <param name="color">The <see cref="DColor" /> to set the embed to</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithColor(DColor color)
	{
		_color = color;
		return this;
	}

	/// <summary>
	///     Sets the title of the embed
	/// </summary>
	/// <param name="title">The title to set on the embed</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithTitle(string title)
	{
		_title = title;
		return this;
	}

	/// <summary>
	///     Sets the author field of the embed
	/// </summary>
	/// <param name="name">The name of the author</param>
	/// <param name="iconUrl">The URL pointing to the icon of the author</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithAuthor(string name, string? iconUrl = null)
	{
		_author = new Author { Name = name, IconUrl = iconUrl };
		return this;
	}

	/// <summary>
	///     Sets the description of the embed
	/// </summary>
	/// <param name="description">The description to set on the embed</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithDescription(string description)
	{
		_description = description;
		return this;
	}

	/// <summary>
	///     Sets the URL pointing to the image to show on the embed
	/// </summary>
	/// <param name="imageUrl">The URL pointing to the image to put on the embed</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithImage(string imageUrl)
	{
		_image = new Image
		{
			Url = imageUrl,
			Height = 200,
			Width = 200
		};

		return this;
	}

	/// <summary>
	///     Sets the thumbnail image URL, the URL should point to the image that you want to use
	/// </summary>
	/// <param name="thumbnailUrl">The URL of the thumbnail</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithThumbnail(string thumbnailUrl)
	{
		_thumbnail = new Thumbnail
		{
			Url = thumbnailUrl
		};
		return this;
	}

	/// <summary>
	///     Set the footer of the embed
	/// </summary>
	/// <param name="text">The text to set on the footer</param>
	/// <param name="iconUrl">The icon URL to set beside the text on the footer</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithFooter(string text, string iconUrl)
	{
		_footer = new Footer
		{
			Text = text,
			IconUrl = iconUrl
		};
		return this;
	}

	/// <summary>
	///     Sets the URL of the embed, this appears on the embed title as a hyperlink to the set URL
	/// </summary>
	/// <param name="url">The URL to set on the embed</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithUrl(string url)
	{
		_url = url;
		return this;
	}

	/// <summary>
	///     Sets the timestamp of the embed, this appears at the bottom of the embed
	/// </summary>
	/// <param name="timestamp">The timestamp to display on the embed</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithTimestamp(DateTime timestamp)
	{
		_timestamp = timestamp;
		return this;
	}

	/// <summary>
	///     Sets a video on the embed using a <see cref="Video" />
	/// </summary>
	/// <param name="video">The <see cref="Video" /> which represents the media to show on the embed</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithVideo(Video video)
	{
		_video = video;
		return this;
	}

	/// <summary>
	///     Sets a video on the embed using a URL
	/// </summary>
	/// <param name="videoUrl">The URL which points to the video to show</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithVideo(string videoUrl)
	{
		_video = new Video { Url = videoUrl, Height = 200, Width = 200 };
		return this;
	}

	/// <summary>
	///     Sets provider to the embed
	/// </summary>
	/// <param name="provider">The provider to set on the embed</param>
	/// <returns>The updated embed builder</returns>
	public EmbedBuilder WithProvider(Provider provider)
	{
		_provider = provider;
		return this;
	}

	/// <summary>
	///     Adds a collection of fields to the embed
	/// </summary>
	/// <param name="fields">The new fields to add to the embed</param>
	/// <returns>The updated <see cref="EmbedBuilder"/></returns>
	public EmbedBuilder AddFieldsRange(IEnumerable<Field> fields)
	{
		_fields.AddRange(fields);
		return this;
	}

	/// <summary>
	///     Builds the <see cref="EmbedBuilder" /> and converts it to a <see cref="Embed" />
	/// </summary>
	/// <returns>The newly constructed embed</returns>
	public Embed Build()
	{
		return new Embed
		{
			Title = _title,
			Description = _description,
			Color = _color ?? Colors.Black,
			Fields = _fields,
			Image = _image,
			Thumbnail = _thumbnail,
			Footer = _footer,
			Url = _url,
			Timestamp = _timestamp?.ToString("O"),
			Video = _video,
			Author = _author,
			Provider = _provider
		};
	}
}