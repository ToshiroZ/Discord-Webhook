using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Discord.Webhook
{
    /// <summary>
    /// The webhook object
    /// </summary>
    [DataContract]
    public class WebhookObject
    {
        /// <summary>
        /// Just a basic constructor
        /// </summary>
        public WebhookObject()
        {
            embeds = new List<Embed>();
        }
        /// <summary>
        /// The message of the embed
        /// </summary>
        [DataMember]
        public string content;
        /// <summary>
        /// the username you shouldnt be able to see this
        /// </summary>
        [DataMember]
        internal string username;
        /// <summary>
        /// the avatar_url you shouldnt be able to see this
        /// </summary>
        [DataMember]
        internal string avatar_url;
        /// <summary>
        /// will the content be read out
        /// </summary>
        [DataMember]
        public bool tts;
        /// <summary>
        /// the embeds to send with the webhook there is a max of 25
        /// </summary>
        [DataMember]
        public List<Embed> embeds;
        /// <summary>
        /// converts the webhookobject to json for posting to the webhook url
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (embeds.Count > 25) throw new Exception("You can only have a maximum of 25 embeds in a message");
            if (embeds.Any(x => x.fields.Count > 25)) throw new Exception("You can only have a maximum of 25 fields in an embed");
            if (content.Length > 1024) throw new Exception("You can only have a maximum of 1024 characters in a message");
            if(embeds.Any(x => x.description.Length > 1024 || x.fields.Any(y => y.value.ToString().Length > 1024))) throw new Exception("You can only have a maximum of 1024 characters in a message");
            foreach (Embed embed in embeds)
            {
                embed.Color = embed.Color == null ? new DColor(0, 0, 0) : embed.Color;
                embed.color = (int)embed.Color.RawValue;
            }
            return JSONSerializer<WebhookObject>.Serialize(this);
        }
    }
    /// <summary>
    /// the embed class
    /// </summary>
    [DataContract]
    public class Embed
    {
        /// <summary>
        /// the embed constructor
        /// </summary>
        public Embed()
        {
            fields = new List<Field>();
        }
        /// <summary>
        /// the title of the embed
        /// </summary>
        [DataMember]
        public string title;
        /// <summary>
        /// the type of embed
        /// </summary>
        [DataMember]
        public string type;
        /// <summary>
        /// the message in the embed
        /// </summary>
        [DataMember]
        public string description;
        /// <summary>
        /// the url the title links to 
        /// </summary>
        [DataMember]
        public string url;
        /// <summary>
        /// the color of the embed 
        /// </summary>
        public DColor Color;
        /// <summary>
        /// the discord int color of the color you shouldnt see this
        /// </summary>
        [DataMember]
        internal int color;
        /// <summary>
        /// the footer of the embed
        /// </summary>
        [DataMember]
        public Footer footer;
        /// <summary>
        /// the image the embed will have 
        /// </summary>
        [DataMember]
        public Image image;
        /// <summary>
        /// the thumbnail of the embed
        /// </summary>
        [DataMember]
        public Thumbnail thumbnail;
        /// <summary>
        /// any videos the embed will play
        /// </summary>
        [DataMember]
        public Video video;
        /// <summary>
        /// the provider of the embed
        /// </summary>
        [DataMember]
        public Provider provider;
        /// <summary>
        /// the author of the embed
        /// </summary>
        [DataMember]
        public Author author;
        /// <summary>
        /// the fields the embed can have capped at 25
        /// </summary>
        [DataMember]
        public List<Field> fields;
    }
    [DataContract]
    public class Field
    {
        /// <summary>
        /// the title of the field
        /// </summary>
        [DataMember]
        public string name;
        /// <summary>
        /// the content of the field
        /// </summary>
        [DataMember]
        public object value;
        /// <summary>
        /// whether the embed is inline
        /// </summary>
        [DataMember]
        public bool inline;
    }
    [DataContract]
    public class Footer
    {
        /// <summary>
        /// the footer text
        /// </summary>
        [DataMember]
        public string text;
        /// <summary>
        /// the url the icon will have
        /// </summary>
        [DataMember]
        public string icon_url;
        /// <summary>
        /// the proxy_icon_url ima be honest no clue but its in the docs so is here
        /// </summary>
        [DataMember]
        public string proxy_icon_url;
    }
    [DataContract]
    public class Image
    {
        /// <summary>
        /// the url of the image
        /// </summary>
        [DataMember]
        public string url;
        /// <summary>
        /// the proxy_url to the image dont ask me its in the docs
        /// </summary>
        [DataMember]
        public string proxy_url;
        /// <summary>
        /// the height in pixels the image will be
        /// </summary>
        [DataMember]
        public int height;
        /// <summary>
        /// the width in pixels the image will be
        /// </summary>
        [DataMember]
        public int width;
    }
    [DataContract]
    public class Thumbnail
    {
        /// <summary>
        /// the url to the image
        /// </summary>
        [DataMember]
        public string url;
        /// <summary>
        /// in the docs no idea
        /// </summary>
        [DataMember]
        public string proxy_url;
        /// <summary>
        /// the height in pixels the image will be
        /// </summary>
        [DataMember]
        public int height;
        /// <summary>
        /// the width in pixels the image will be
        /// </summary>
        [DataMember]
        public int width;
    }
    [DataContract]
    public class Video
    {
        /// <summary>
        /// the url to the video
        /// </summary>
        [DataMember]
        public string url;
        /// <summary>
        /// the height in pixels the videp will be
        /// </summary>
        [DataMember]
        public int height;
        /// <summary>
        /// the width in pixels the video will be
        /// </summary>
        [DataMember]
        public int width;
    }
    [DataContract]
    public class Provider
    {
        /// <summary>
        /// the provider name
        /// </summary>
        [DataMember]
        public string name;
        /// <summary>
        /// the url to the provider
        /// </summary>
        [DataMember]
        public string url;
    }
    [DataContract]
    public class Author
    {
        /// <summary>
        /// the authors name
        /// </summary>
        [DataMember]
        public string name;
        /// <summary>
        /// the url the author should link to
        /// </summary>
        [DataMember]
        public string url;
        /// <summary>
        /// the authors icon 
        /// </summary>
        [DataMember]
        public string icon_url;
        [DataMember]
        public string proxy_icon_url;
    }
    public class DColor
    {
        internal uint RawValue { get; }
        /// <summary>
        /// Creates a new color to send to discord
        /// The reason it is not System.Drawing.Color is to ensure compat with the color in the media and to support more people heading forward
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public DColor(int r, int g, int b)
        {
            if (r < 0 || r > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(r), "Value must be within [0,255].");
            }
            if (g < 0 || g > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(g), "Value must be within [0,255].");
            }
            if (b < 0 || b > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(b), "Value must be within [0,255].");
            }
            RawValue =
                ((uint)r << 16) |
                ((uint)g << 8) |
                (uint)b;
        }
    }
}
