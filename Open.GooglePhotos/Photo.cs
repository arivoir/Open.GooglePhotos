using Open.Google;
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Open.GooglePhotos
{
    public class Photo
    {
        public string Id { get; set; }
        public string AlbumId { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public string ETag { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Published { get; set; }
        public MediaThumbnail[] Thumbnails { get; set; }
        public MediaContent Content { get; set; }
        public string Link { get; set; }
        public string PhotoUri { get; set; }
        public string Summary { get; set; }
        public GeoRss Where { get; set; }
        public string Keywords { get; set; }
        public Access Access { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }

        internal static Photo Parse(XElement entry)
        {
            var id = entry.Element(XName.Get("id", Namespaces.AtomNS));
            var photoId = entry.Element(XName.Get("id", Namespaces.PhotosNS));
            var albumId = entry.Element(XName.Get("albumid", Namespaces.PhotosNS));
            var title = entry.Element(XName.Get("title", Namespaces.AtomNS));
            var etag = entry.Attribute(XName.Get("etag", Namespaces.GdNS));
            var updated = entry.Element(XName.Get("updated", Namespaces.AtomNS));
            var published = entry.Element(XName.Get("published", Namespaces.AtomNS));
            var access = entry.Element(XName.Get("access", Namespaces.PhotosNS));
            var width = entry.Element(XName.Get("width", Namespaces.PhotosNS));
            var height = entry.Element(XName.Get("height", Namespaces.PhotosNS));
            var size = entry.Element(XName.Get("size", Namespaces.PhotosNS));
            var commentingEnabled = entry.Element(XName.Get("commentingEnabled", Namespaces.PhotosNS));
            var commentCount = entry.Element(XName.Get("commentCount", Namespaces.PhotosNS));

            var group = entry.Element(XName.Get("group", Namespaces.MediaNS));
            var thumbnails = group != null ? group.Elements(XName.Get("thumbnail", Namespaces.MediaNS)).Select(e => MediaThumbnail.Parse(e)).ToArray() : new MediaThumbnail[0];
            var description = group != null ? group.Element(XName.Get("description", Namespaces.MediaNS)) : null;
            var content = group != null ? group.Element(XName.Get("content", Namespaces.MediaNS)) : null;
            var keywords = group != null ? group.Element(XName.Get("keywords", Namespaces.MediaNS)) : null;
            var alternate = entry.Elements(XName.Get("link", Namespaces.AtomNS)).FirstOrDefault(e => e.Attribute("rel") != null && e.Attribute("rel").Value == "alternate");
            var where = entry.Element(XName.Get("where", Namespaces.GeoRSSNS));
            return new Photo
            {
                Id = photoId != null ? photoId.Value : null,
                AlbumId = albumId != null ? albumId.Value : null,
                Title = title != null ? title.Value : null,
                Updated = updated != null ? DateTime.Parse(updated.Value, CultureInfo.InvariantCulture.DateTimeFormat) : (DateTime?)null,
                Published = published != null ? DateTime.Parse(published.Value, CultureInfo.InvariantCulture.DateTimeFormat) : (DateTime?)null,
                Width = width != null ? int.Parse(width.Value) : 0,
                Height = height != null ? int.Parse(height.Value) : 0,
                Uri = id != null ? id.Value : null,
                Link = alternate != null && alternate.Attribute("href") != null ? alternate.Attribute("href").Value : null,
                ETag = etag != null ? etag.Value : "",
                Thumbnails = thumbnails,
                Content = content != null ? MediaContent.Parse(content) : null,
                Access = access != null ? Album.ParseAccess(access.Value) : Access.Unknown,
                Keywords = keywords != null ? keywords.Value : null,
                Where = where != null ? GeoRss.Parse(where) : null,
                Summary = description != null ? description.Value : null,
                Size = size != null ? int.Parse(size.Value) : 0,
            };
        }
    }
}
