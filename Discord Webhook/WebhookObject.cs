using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Discord_Webhook
{
    [DataContract]
    public class WebhookObject
    {
        [DataMember]
        public string content;
        [DataMember]
        public string username;
        [DataMember]
        public string avatar_url;
        [DataMember]
        public bool tts;
        [DataMember]
        public Embed[] embeds;
        [DataMember]
        public string payload_json;
        public override string ToString()
        {
            return JSONSerializer<WebhookObject>.Serialize(this);
        }
    }
    [DataContract]
    public class Embed
    {
        [DataMember]
        public string title;
        [DataMember]
        public string type;
        [DataMember]
        public string description;
        [DataMember]
        public string url;
        [DataMember]
        public int color;
        [DataMember]
        public Footer footer;
        [DataMember]
        public Image image;
        [DataMember]
        public Thumbnail thumbnail;
        [DataMember]
        public Video video;
        [DataMember]
        public Provider provider;
        [DataMember]
        public Author author;
        [DataMember]
        public Field[] fields;
    }
    [DataContract]
    public class Field
    {
        [DataMember]
        public string name;
        [DataMember]
        public string value;
        [DataMember]
        public bool inline;
    }
    [DataContract]
    public class Footer
    {
        [DataMember]
        public string text;
        [DataMember]
        public string icon_url;
        [DataMember]
        public string proxy_icon_url;
    }
    [DataContract]
    public class Image
    {
        [DataMember]
        public string url;
        [DataMember]
        public string proxy_url;
        [DataMember]
        public int height;
        [DataMember]
        public int width;
    }
    [DataContract]
    public class Thumbnail
    {
        [DataMember]
        public string url;
        [DataMember]
        public string proxy_url;
        [DataMember]
        public int height;
        [DataMember]
        public int width;
    }
    [DataContract]
    public class Video
    {
        [DataMember]
        public string url;
        [DataMember]
        public int height;
        [DataMember]
        public int width;
    }
    [DataContract]
    public class Provider
    {
        [DataMember]
        public string name;
        [DataMember]
        public string url;
    }
    [DataContract]
    public class Author
    {
        [DataMember]
        public string name;
        [DataMember]
        public string url;
        [DataMember]
        public string icon_url;
        [DataMember]
        public string proxy_icon_url;
    }
}
