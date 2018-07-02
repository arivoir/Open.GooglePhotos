using Open.Google;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Open.GooglePhotos
{
    public class Album
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public string ETag { get; set; }
        public string Link { get; set; }
        public uint NumPhotos { get; set; }
        public Access Access { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public MediaContent Content { get; set; }
        public MediaThumbnail Thumbnail { get; set; }

        internal static Album Parse(System.Xml.Linq.XElement entry)
        {
            var id = entry.Element(XName.Get("id", Namespaces.AtomNS));
            var albumId = entry.Element(XName.Get("id", Namespaces.PhotosNS));
            var title = entry.Element(XName.Get("title", Namespaces.AtomNS));
            var group = entry.Element(XName.Get("group", Namespaces.MediaNS));
            var description = group != null ? group.Element(XName.Get("description", Namespaces.MediaNS)) : null;
            var content = group != null ? group.Element(XName.Get("content", Namespaces.MediaNS)) : null;
            var thumbnail = group != null ? group.Element(XName.Get("thumbnail", Namespaces.MediaNS)) : null;
            var etag = entry.Attribute(XName.Get("etag", Namespaces.GdNS));
            var numPhotos = entry.Element(XName.Get("numphotos", Namespaces.PhotosNS));
            var albumType = entry.Element(XName.Get("albumType", Namespaces.PhotosNS));
            var location = entry.Element(XName.Get("location", Namespaces.PhotosNS));
            var access = entry.Element(XName.Get("access", Namespaces.PhotosNS));
            var alternate = entry.Elements(XName.Get("link", Namespaces.AtomNS)).FirstOrDefault(e => e.Attribute("rel") != null && e.Attribute("rel").Value == "alternate");
            return new Album
            {
                Id = albumId != null ? albumId.Value : null,
                Uri = id != null ? id.Value : null,
                Link = alternate != null && alternate.Attribute("href") != null ? alternate.Attribute("href").Value : null,
                Title = title != null ? title.Value : null,
                Description = description != null ? description.Value : null,
                ETag = etag != null ? etag.Value : null,
                NumPhotos = numPhotos != null ? uint.Parse(numPhotos.Value) : 0,
                Access = access != null ? ParseAccess(access.Value) : Access.Unknown,
                Location = location != null ? location.Value : null,
                Type = albumType != null ? albumType.Value : "",
                Content = content != null ? MediaContent.Parse(content) : null,
                Thumbnail = thumbnail != null ? MediaThumbnail.Parse(thumbnail) : null,
            };
        }

        public static Access ParseAccess(string access)
        {
            switch (access)
            {
                case "public":
                    return Access.Public;
                case "private":
                case "only_you":
                    return Access.Private;
                case "protected":
                    return Access.Protected;
                default:
                    return Access.Unknown;
            }
        }
    }

    public enum Access
    {
        Private,
        Public,
        Protected,
        Unknown
    }
}
