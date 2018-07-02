using Open.Google;
using System.Xml.Linq;

namespace Open.GooglePhotos
{
    public class Metadata
    {
        public string User { get; set; }
        public string Nickname { get; set; }
        public string Thumbnail { get; set; }
        public long? QuotaCurrent { get; set; }
        public long? QuotaLimit { get; set; }

        #region ** implementation

        public static Metadata Parse(XDocument doc)
        {
            var user = doc.Root.Element(XName.Get("user", Namespaces.PhotosNS));
            var nickname = doc.Root.Element(XName.Get("nickname", Namespaces.PhotosNS));
            var thumbnail = doc.Root.Element(XName.Get("thumbnail", Namespaces.PhotosNS));
            var quotaCurrent = doc.Root.Element(XName.Get("quotacurrent", Namespaces.PhotosNS));
            var quotaLimit = doc.Root.Element(XName.Get("quotalimit", Namespaces.PhotosNS));
            return new Metadata
            {
                User = user != null ? user.Value : null,
                Nickname = nickname != null ? nickname.Value : null,
                Thumbnail = thumbnail != null ? thumbnail.Value : null,
                QuotaCurrent = quotaCurrent != null ? long.Parse(quotaCurrent.Value) : (long?)null,
                QuotaLimit = quotaLimit != null ? long.Parse(quotaLimit.Value) : (long?)null,
            };
        }

        #endregion
    }
}
