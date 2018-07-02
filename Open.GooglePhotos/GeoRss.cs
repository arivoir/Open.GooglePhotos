using Open.Google;
using System.Globalization;
using System.Xml.Linq;

namespace Open.GooglePhotos
{
    public class GeoRss
    {
        public GmlEnvelope Envelope { get; set; }
        public GmlPoint Point { get; set; }

        public static GeoRss Parse(XElement where)
        {
            var envelope = where.Element(XName.Get("Envelope", Namespaces.GmlNS));
            var point = where.Element(XName.Get("Point", Namespaces.GmlNS));

            return new GeoRss
            {
                Envelope = envelope != null ? GmlEnvelope.Parse(envelope) : null,
                Point = GmlPoint.Parse(point),
            };
        }
    }

    public class GmlEnvelope
    {
        public double LowerCornerX { get; set; }
        public double LowerCornerY { get; set; }
        public double UpperCornerX { get; set; }
        public double UpperCornerY { get; set; }

        public static GmlEnvelope Parse(XElement envelope)
        {
            var lowerCorner = envelope.Element(XName.Get("lowerCorner", Namespaces.GmlNS));
            var upperCorner = envelope.Element(XName.Get("upperCorner", Namespaces.GmlNS));
            var lowerCornerParts = lowerCorner.Value.Split(' ');
            var upperCornerParts = upperCorner.Value.Split(' ');
            return new GmlEnvelope
            {
                LowerCornerX = double.Parse(lowerCornerParts[0], CultureInfo.InvariantCulture.NumberFormat),
                LowerCornerY = double.Parse(lowerCornerParts[1], CultureInfo.InvariantCulture.NumberFormat),
                UpperCornerX = double.Parse(upperCornerParts[0], CultureInfo.InvariantCulture.NumberFormat),
                UpperCornerY = double.Parse(upperCornerParts[1], CultureInfo.InvariantCulture.NumberFormat),
            };
        }
    }

    public class GmlPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static GmlPoint Parse(XElement point)
        {
            var pos = point.Element(XName.Get("pos", Namespaces.GmlNS));
            var parts = pos.Value.Split(' ');
            return new GmlPoint
            {
                X = double.Parse(parts[0], CultureInfo.InvariantCulture.NumberFormat),
                Y = double.Parse(parts[1], CultureInfo.InvariantCulture.NumberFormat),
            };
        }
    }
}
