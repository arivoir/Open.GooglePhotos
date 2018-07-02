using Open.Google;
using System;
using System.Globalization;
using System.Xml.Linq;

namespace Open.GooglePhotos
{
    public class Comment
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public CommentAuthor Author { get; set; }
        public DateTime Published { get; set; }

        internal static Comment Parse(XElement e)
        {
            var content = e.Element(XName.Get("content", Namespaces.AtomNS));
            var id = e.Element(XName.Get("id", Namespaces.PhotosNS));
            var author = e.Element(XName.Get("author", Namespaces.AtomNS));
            var published = e.Element(XName.Get("published", Namespaces.AtomNS));

            return new Comment
            {
                Id = id.Value,
                Type = content.Attribute("type") != null ? content.Attribute("type").Value : "",
                Value = content.Value,
                Author = CommentAuthor.Parse(author),
                Published = DateTime.Parse(published.Value, CultureInfo.InvariantCulture.DateTimeFormat),
            };
        }

    }
}
