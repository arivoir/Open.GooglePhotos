using Open.Google;
using System.Xml.Linq;

namespace Open.GooglePhotos
{
    public class CommentAuthor
    {
        public string User { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }

        internal static CommentAuthor Parse(XElement e)
        {
            var user = e.Element(XName.Get("user", Namespaces.PhotosNS));
            var nickname = e.Element(XName.Get("nickname", Namespaces.PhotosNS));
            var thumbnail = e.Element(XName.Get("thumbnail", Namespaces.PhotosNS));

            return new CommentAuthor
            {
                User = user.Value,
                Name = nickname.Value,
                Thumbnail = thumbnail.Value,
            };
        }
    }
}
