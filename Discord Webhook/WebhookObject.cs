namespace Discord.Webhook;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

/// <summary>
///     Represents a webhook message object allowing configuration of plaintext messages, images, or embeds.
///     This class can be utilized within the Discord webhook framework to structure content sent to webhooks.
/// </summary>
[DataContract]
public class WebhookObject
{
    /// <summary>
    ///     The webhook message object, allows you to set plaintext messages, images or embeds
    /// </summary>
    public WebhookObject()
    {
        Embeds = [];
        Content = null;
    }

    /// <summary>
    ///     The message of the embed
    /// </summary>
    [DataMember(Name = "content")] public string? Content;

    /// <summary>
    ///     Represents the username for the webhook message.
    /// </summary>
    [DataMember(Name = "username")] internal string? Username;

    /// <summary>
    ///     The avatar URL for the webhook sender. This is an internal field that should not be exposed externally.
    /// </summary>
    [DataMember(Name = "avatar_url")] internal string? AvatarUrl;

    /// <summary>
    ///     The name of the thread to create (requires the webhook channel to be a forum channel)
    /// </summary>
    [DataMember(Name = "thread_name")] internal string? ThreadName;

    /// <summary>
    ///     Indicates whether the content will be read out loud using text-to-speech.
    /// </summary>
    [DataMember(Name = "tts")] public bool IsTextToSpeech;

    /// <summary>
    ///     The collection of embeds to send with the webhook, with a maximum limit of 25 embeds per message.
    /// </summary>
    [DataMember(Name = "embeds")] public List<Embed> Embeds;

    /// <summary>
    ///     Converts the <see cref="WebhookObject" /> to a JSON string representation
    ///     for posting to a webhook URL.
    /// </summary>
    /// <returns>A JSON string representing the <see cref="WebhookObject" />.</returns>
    public override string ToString()
    {
        // Set the content, ensure it is never null
        Content ??= string.Empty;

        // Check if more than 25 embeds have been added
        if (Embeds.Count > 25) throw new Exception("You can only have a maximum of 25 embeds in a message");

        // Check if any of the embeds have more than 25 fields
        if (Embeds.Any(x => x.Fields.Count > 25))
            throw new Exception("You can only have a maximum of 25 fields in an embed");

        // Check if the message has a content length of more than 1024 characters
        if (Content.Length > 1024)
            throw new Exception("You can only have a maximum of 1024 characters in a message");

        // Check if any of the embeds have a description or any fields that are longer than 1024 characters
        if (Embeds.Any(x => x.Description?.Length > 1024 || x.Fields.Any(y => y.Value.ToString().Length > 1024)))
            throw new Exception("You can only have a maximum of 1024 characters in a message");

        // Loop through and set the colors, default value being black
        foreach (var embed in Embeds) embed.ColorInt = (int)embed.Color.RawValue;

        // Serialize the object to JSON and return
        return JsonSerializer<WebhookObject>.Serialize(this);
    }

    /// <summary>
    ///     Adds an embed using the <see cref="EmbedBuilder" />.
    /// </summary>
    /// <param name="builder">The builder to use when adding the embed.</param>
    /// <returns>The updated <see cref="WebhookObject" /> with the new embed.</returns>
    public WebhookObject AddEmbed(EmbedBuilder builder)
    {
        Embeds.Add(builder.Build());
        return this;
    }

    /// <summary>
    ///     Adds an embed
    /// </summary>
    /// <param name="embed">The embed to add to the collection</param>
    /// <returns>The updated <see cref="WebhookObject" /> with the new embed</returns>
    public WebhookObject AddEmbed(Embed embed)
    {
        Embeds.Add(embed);
        return this;
    }

    /// <summary>
    ///     Adds an embed to the webhook object using a specified <see cref="Action{T}" /> to configure the
    ///     <see cref="EmbedBuilder" />.
    /// </summary>
    /// <param name="builder">An action that configures the <see cref="EmbedBuilder" /> with desired properties.</param>
    /// <returns>The instance of the <see cref="WebhookObject" /> with the newly added embed.</returns>
    public WebhookObject AddEmbed(Action<EmbedBuilder> builder)
    {
        var embedBuilder = new EmbedBuilder();
        builder(embedBuilder);
        AddEmbed(embedBuilder);

        return this;
    }
}

/// <summary>
///     Represents an embed structure used in webhook messages, including fields such as title, description, color, and
///     other media elements.
/// </summary>
[DataContract]
public class Embed
{
    /// <summary>
    ///     The title of the embed
    /// </summary>
    [DataMember(Name = "title")] public string? Title;

    /// <summary>
    ///     The type of the embed
    /// </summary>
    [DataMember(Name = "type")] public string? Type;

    /// <summary>
    ///     The message in the embed
    /// </summary>
    [DataMember(Name = "description")] public string? Description;

    /// <summary>
    ///     The URL that the embed title links to
    /// </summary>
    [DataMember(Name = "url")] public string? Url;

    /// <summary>
    ///     The timestamp of the embed
    /// </summary>
    [DataMember(Name = "timestamp")] public string? Timestamp;

    /// <summary>
    ///     Represents the color of the embed
    /// </summary>
    public DColor Color = Colors.Black;

    /// <summary>
    ///     The internal integer representation of the embed's color.
    /// </summary>
    [DataMember(Name = "color")] internal int ColorInt;

    /// <summary>
    ///     The footer of the embed
    /// </summary>
    [DataMember(Name = "footer")] public Footer? Footer;

    /// <summary>
    ///     The image associated with the embed
    /// </summary>
    [DataMember(Name = "image")] public Image? Image;

    /// <summary>
    ///     The thumbnail of the embed
    /// </summary>
    [DataMember(Name = "thumbnail")] public Thumbnail? Thumbnail;

    /// <summary>
    ///     Any videos the embed will play
    /// </summary>
    [DataMember(Name = "video")] public Video? Video;

    /// <summary>
    ///     The provider of the embed
    /// </summary>
    [DataMember(Name = "provider")] public Provider? Provider;

    /// <summary>
    ///     The author of the embed
    /// </summary>
    [DataMember(Name = "author")] public Author? Author;

    /// <summary>
    ///     The fields the embed can have capped at 25
    /// </summary>
    [DataMember(Name = "fields")] public List<Field> Fields = [];
}

/// <summary>
///     Represents a field in an embed, with a name, value, and inline display option.
/// </summary>
[DataContract]
public class Field
{
    /// <summary>
    ///     The title of the field
    /// </summary>
    [DataMember(Name = "name")] public string Name = null!;

    /// <summary>
    ///     The content of the field
    /// </summary>
    [DataMember(Name = "value")] public object Value = null!;

    /// <summary>
    ///     Whether the embed field is displayed inline with other fields
    /// </summary>
    [DataMember(Name = "inline")] public bool IsInline;
}

/// <summary>
///     Represents the footer section of an embed, containing text and optional icon URLs.
/// </summary>
[DataContract]
public class Footer
{
    /// <summary>
    ///     The text content of the footer
    /// </summary>
    [DataMember(Name = "text")] public string Text = null!;

    /// <summary>
    ///     The URL for the footer icon
    /// </summary>
    [DataMember(Name = "icon_url")] public string? IconUrl;

    /// <summary>
    ///     The proxy URL for the icon.
    /// </summary>
    [DataMember(Name = "proxy_icon_url")] public string? ProxyIconUrl;
}

/// <summary>
///     Represents an image with properties such as URL, proxy URL, height, and width for use in an embed.
/// </summary>
[DataContract]
public class Image
{
    /// <summary>
    ///     The URL of the image
    /// </summary>
    [DataMember(Name = "url")] public string Url = null!;

    /// <summary>
    ///     A proxied URL of the image
    /// </summary>
    [DataMember(Name = "proxy_url")] public string? ProxyUrl;

    /// <summary>
    ///     The height in pixels the image will be
    /// </summary>
    [DataMember(Name = "height")] public int? Height;

    /// <summary>
    ///     The width in pixels of the image
    /// </summary>
    [DataMember(Name = "width")] public int? Width;
}

/// <summary>
///     Represents the thumbnail image of an embed, including attributes for image URL, proxy URL, and dimensions.
/// </summary>
[DataContract]
public class Thumbnail
{
    /// <summary>
    ///     The URL to the image used in the thumbnail
    /// </summary>
    [DataMember(Name = "url")] public string Url = null!;

    /// <summary>
    ///     A proxied URL of the image, used to access the image in certain network configurations.
    /// </summary>
    [DataMember(Name = "proxy_url")] public string? ProxyUrl;

    /// <summary>
    ///     The height in pixels the image will be
    /// </summary>
    [DataMember(Name = "height")] public int? Height;

    /// <summary>
    ///     The width in pixels the image will be
    /// </summary>
    [DataMember(Name = "width")] public int? Width;
}

/// <summary>
///     Represents a video object within an embed, containing properties like URL, height, and width.
/// </summary>
[DataContract]
public class Video
{
    /// <summary>
    ///     The URL associated with the embed or video
    /// </summary>
    [DataMember(Name = "url")] public string? Url;

    /// <summary>
    ///     The proxy URL of the video
    /// </summary>
    [DataMember(Name = "proxy_url")] public string? ProxyUrl;

    /// <summary>
    ///     The height in pixels that the video will be displayed
    /// </summary>
    [DataMember(Name = "height")] public int? Height;

    /// <summary>
    ///     The width in pixels the video will be
    /// </summary>
    [DataMember(Name = "width")] public int? Width;
}

/// <summary>
///     Represents the provider of the embed, including its name and URL.
/// </summary>
[DataContract]
public class Provider
{
    /// <summary>
    ///     The provider name
    /// </summary>
    [DataMember(Name = "name")] public string? Name;

    /// <summary>
    ///     The URL associated with the embed
    /// </summary>
    [DataMember(Name = "url")] public string? Url;
}

/// <summary>
///     Represents the author information of an embed, including name, URL, and icon details.
/// </summary>
[DataContract]
public class Author
{
    /// <summary>
    ///     The name associated with the author
    /// </summary>
    [DataMember(Name = "name")] public string Name = null!;

    /// <summary>
    ///     The URL associated with the embed, used to provide a hyperlink for the embed title
    /// </summary>
    [DataMember(Name = "url")] public string? Url;

    /// <summary>
    ///     The URL to the author's icon
    /// </summary>
    [DataMember(Name = "icon_url")] public string? IconUrl;

    /// <summary>
    ///     A proxied URL of the author's icon. Useful for displaying images behind a content delivery network.
    /// </summary>
    [DataMember(Name = "proxy_icon_url")] public string? ProxyIconUrl;
}

/// <summary>
///     Represents a color used in Discord embeds to ensure compatibility with media and broader support.
/// </summary>
public class DColor
{
    /// <summary>
    ///     Represents the raw color value as a 32-bit unsigned integer.
    /// </summary>
    internal uint RawValue { get; }

    /// <summary>
    ///     Represents a color value to be used in Discord webhooks.
    ///     The class provides support for defining colors using RGB values.
    /// </summary>
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
    ///     Represents a custom color structure used in Discord webhooks, providing compatibility with media colors.
    /// </summary>
    public DColor(byte r, byte g, byte b)
    {
        RawValue =
            ((uint)r << 16) |
            ((uint)g << 8) |
            b;
    }
}