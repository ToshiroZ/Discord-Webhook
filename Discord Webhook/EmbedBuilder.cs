using System.Collections.Generic;

namespace Discord.Webhook
{
    public class EmbedBuilder
    {
        private string _title;
        private string _description;
        private string _url;
        private Image _image;
        private Thumbnail _thumbnail;
        private readonly List<Field> _fields;
        private Footer _footer;
        private DColor _color;
        private Video _video;
        private Author _author;
        private Provider _provider;

        public EmbedBuilder()
        {
            _fields = new List<Field>();
            _color = Colors.Black;
        }

        /// <summary>
        /// Adds a field to the embed
        /// </summary>
        /// <param name="name">The title of the field</param>
        /// <param name="value">the content in the field</param>
        /// <param name="inline"></param>
        /// <returns></returns>
        public EmbedBuilder AddField(string name, object value, bool inline = false)
        {
            _fields.Add(new Field()
            {
                name = name,
                value = value,
                inline = inline
            });

            return this;
        }
        /// <summary>
        /// Sets color of the embed
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public EmbedBuilder WithColor(DColor color)
        {
            _color = color;
            return this;
        }
        /// <summary>
        /// Sets the title of the embed
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public EmbedBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        /// <summary>
        /// Sets the author field of the embed
        /// </summary>
        /// <param name="name">The name of the author</param>
        /// <param name="iconUrl">The icon of the author</param>
        /// <returns></returns>
        public EmbedBuilder WithAuthor(string name, string iconUrl = null)
        {
            _author = new Author() {name = name, icon_url = iconUrl};
            return this;
        }
         /// <summary>
         /// Sets the description of the embed
         /// </summary>
         /// <param name="description"></param>
         /// <returns></returns>
        public EmbedBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
        /// <summary>
        /// Sets the image of the url
        /// </summary>
        /// <param name="urlImagen"></param>
        /// <returns></returns>
        public EmbedBuilder WithImage(string urlImagen)
        {
            _image = new Image()
            {
                url = urlImagen,
                height = 200,
                width = 200
            };
            return this;
        }
        /// <summary>
        /// Sets the image of the thumbnail of the url 
        /// </summary>
        /// <param name="urlThumbnail"></param>
        /// <returns></returns>
        public EmbedBuilder WithThumbnail(string urlThumbnail)
        {
            _thumbnail = new Thumbnail()
            {
                url = urlThumbnail
            };
            return this;
        }

        /// <summary>
        /// Set the footer of the embed
        /// </summary>
        /// <param name="text"></param>
        /// <param name="iconUrl"></param>
        /// <returns></returns>
        public EmbedBuilder WithFooter(string text, string iconUrl)
        {
            _footer = new Footer()
            {
                text = text,
                icon_url = iconUrl
            };
            return this;
        }

        /// <summary>
        /// Sets the url of the embed
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public EmbedBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        /// <summary>
        /// Sets a video to the embed
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public EmbedBuilder WithVideo(Video video)
        {
            _video = video;
            return this;
        }

        /// <summary>
        /// Sets a video to the embed with url
        /// </summary>
        /// <param name="videoUrl"></param>
        /// <returns></returns>
        public EmbedBuilder WithVideo(string videoUrl)
        {
            _video = new Video() {url = videoUrl, height = 200, width = 200};
            return this;
        }

        /// <summary>
        /// Sets provider to the embed
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public EmbedBuilder WithProvider(Provider provider)
        {
            _provider = provider;
            return this;
        }

        /// <summary>
        /// Adds a collection of fields to the embed
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public EmbedBuilder AddFieldsRange(IEnumerable<Field> fields)
        {
            _fields.AddRange(fields);
            return this;
        }

        /// <summary>
        /// BUILDS THAT EMBED
        /// </summary>
        /// <returns></returns>
        public Embed Build()
        {
            return new Embed()
            {
                title = _title,
                description = _description,
                Color = _color,
                fields = _fields,
                image = _image,
                thumbnail = _thumbnail,
                footer = _footer,
                url = _url,
                video = _video,
                author = _author,
                provider = _provider
            };
        }
    }
}