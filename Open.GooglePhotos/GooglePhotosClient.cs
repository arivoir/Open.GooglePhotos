using Open.Google;
using Open.IO;
using Open.Net.Http;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Open.GooglePhotos
{
    public class GooglePhotosClient : GoogleClient
    {
        #region ** fields

        public const string SCOPE = "https://picasaweb.google.com/data/";
        private string _oauth2Token;

        #endregion

        #region ** initialization

        public GooglePhotosClient(string oauth2Token)
        {
            _oauth2Token = oauth2Token;
        }

        #endregion

        #region ** authentication

        public static string GetRequestUrl(string clientId, string callbackUrl)
        {
            return GoogleClient.GetRequestUrl(clientId, SCOPE, callbackUrl);
        }

        #endregion

        #region ** public methods

        public async Task<Metadata> GetMetadataAsync(string fields = null)
        {
            var uri = BuildUri("https://picasaweb.google.com/data/feed/api/user/default", fields: fields);
            var client = CreateClient();

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var doc = XDocument.Load(await response.Content.ReadAsStreamAsync());
                return Metadata.Parse(doc);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<GoogleFeed<Album>> GetAlbumsAsync(string fields = null, int? startIndex = null, int? maxResults = null, string orderby = null)
        {
            var uri = BuildUri("https://picasaweb.google.com/data/feed/api/user/default", startIndex: startIndex, fields: fields, maxResults: maxResults, orderby: orderby);
            var client = CreateClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var doc = XDocument.Load(await response.Content.ReadAsStreamAsync());
                var feed = doc.Element(XName.Get("feed", Namespaces.AtomNS));
                var iconElem = feed.Element(XName.Get("icon", Namespaces.AtomNS));
                var authorElem = feed.Element(XName.Get("author", Namespaces.AtomNS));
                var totalResultsElem = feed.Element(XName.Get("totalResults", Namespaces.OpenSearchNS));
                var startIndex2Elem = feed.Element(XName.Get("startIndex", Namespaces.OpenSearchNS));
                var itemsPerPageElem = feed.Element(XName.Get("itemsPerPage", Namespaces.OpenSearchNS));

                var icon = iconElem != null ? iconElem.Value : null;
                var author = authorElem != null ? Author.Parse(authorElem) : null;
                var totalResults = totalResultsElem != null ? int.Parse(totalResultsElem.Value) : -1;
                var startIndex2 = startIndex2Elem != null ? int.Parse(startIndex2Elem.Value) : -1;
                var itemsPerPage = itemsPerPageElem != null ? int.Parse(itemsPerPageElem.Value) : -1;
                var items = feed.Elements(XName.Get("entry", Namespaces.AtomNS)).Select(e => Album.Parse(e)).ToList();
                return new GoogleFeed<Album>(icon, author, totalResults, startIndex2, itemsPerPage, items);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<GoogleFeed<Photo>> GetPhotosFromAlbumAsync(string albumId, string fields = null, int? startIndex = null, int? maxResults = null, string orderby = null)
        {
            var uri = BuildUri(string.Format("https://picasaweb.google.com/data/feed/api/user/default/albumid/{0}", albumId), fields: fields, startIndex: startIndex, maxResults: maxResults, orderby: orderby);
            var client = CreateClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var doc = XDocument.Load(await response.Content.ReadAsStreamAsync());
                var feed = doc.Element(XName.Get("feed", Namespaces.AtomNS));
                var iconElem = feed.Element(XName.Get("icon", Namespaces.AtomNS));
                var authorElem = feed.Element(XName.Get("author", Namespaces.AtomNS));
                var totalResultsElem = feed.Element(XName.Get("totalResults", Namespaces.OpenSearchNS));
                var startIndex2Elem = feed.Element(XName.Get("startIndex", Namespaces.OpenSearchNS));
                var itemsPerPageElem = feed.Element(XName.Get("itemsPerPage", Namespaces.OpenSearchNS));

                var icon = iconElem != null ? iconElem.Value : null;
                var author = authorElem != null ? Author.Parse(authorElem) : null;
                var totalResults = totalResultsElem != null ? int.Parse(totalResultsElem.Value) : -1;
                var startIndex2 = startIndex2Elem != null ? int.Parse(startIndex2Elem.Value) : -1;
                var itemsPerPage = itemsPerPageElem != null ? int.Parse(itemsPerPageElem.Value) : -1;
                var items = feed.Elements(XName.Get("entry", Namespaces.AtomNS)).Select(e => Photo.Parse(e)).ToList();
                return new GoogleFeed<Photo>(icon, author, totalResults, startIndex2, itemsPerPage, items);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<GoogleFeed<Photo>> GetPhotosAsync(string q, string fields = null, int? startIndex = null, int? maxResults = null, string orderby = null)
        {
            var uri = BuildUri("https://picasaweb.google.com/data/feed/api/user/default", q: q, kind: "photo", fields: fields, startIndex: startIndex, maxResults: maxResults, orderby: orderby);
            var client = CreateClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var doc = XDocument.Load(await response.Content.ReadAsStreamAsync());
                var feed = doc.Element(XName.Get("feed", Namespaces.AtomNS));
                var iconElem = feed.Element(XName.Get("icon", Namespaces.AtomNS));
                var authorElem = feed.Element(XName.Get("author", Namespaces.AtomNS));
                var totalResultsElem = feed.Element(XName.Get("totalResults", Namespaces.OpenSearchNS));
                var startIndex2Elem = feed.Element(XName.Get("startIndex", Namespaces.OpenSearchNS));
                var itemsPerPageElem = feed.Element(XName.Get("itemsPerPage", Namespaces.OpenSearchNS));

                var icon = iconElem != null ? iconElem.Value : null;
                var author = authorElem != null ? Author.Parse(authorElem) : null;
                var totalResults = totalResultsElem != null ? int.Parse(totalResultsElem.Value) : -1;
                var startIndex2 = startIndex2Elem != null ? int.Parse(startIndex2Elem.Value) : -1;
                var itemsPerPage = itemsPerPageElem != null ? int.Parse(itemsPerPageElem.Value) : -1;
                var items = feed.Elements(XName.Get("entry", Namespaces.AtomNS)).Select(e => Photo.Parse(e)).ToList();
                return new GoogleFeed<Photo>(icon, author, totalResults, startIndex2, itemsPerPage, items);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<GoogleFeed<Comment>> GetPhotoCommentsAsync(string photoId, int? startIndex = null, int? maxResults = null, string orderby = null)
        {
            var uri = BuildUri(string.Format("https://picasaweb.google.com/data/feed/api/user/default/photoid/{0}?kind=comment", photoId), startIndex: startIndex, maxResults: maxResults, orderby: orderby);
            var client = CreateClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var doc = XDocument.Load(await response.Content.ReadAsStreamAsync());
                var feed = doc.Element(XName.Get("feed", Namespaces.AtomNS));
                var iconElem = feed.Element(XName.Get("icon", Namespaces.AtomNS));
                var authorElem = feed.Element(XName.Get("author", Namespaces.AtomNS));
                var totalResultsElem = feed.Element(XName.Get("totalResults", Namespaces.OpenSearchNS));
                var startIndex2Elem = feed.Element(XName.Get("startIndex", Namespaces.OpenSearchNS));
                var itemsPerPageElem = feed.Element(XName.Get("itemsPerPage", Namespaces.OpenSearchNS));

                var icon = iconElem != null ? iconElem.Value : null;
                var author = authorElem != null ? Author.Parse(authorElem) : null;
                var totalResults = totalResultsElem != null ? int.Parse(totalResultsElem.Value) : -1;
                var startIndex2 = startIndex2Elem != null ? int.Parse(startIndex2Elem.Value) : -1;
                var itemsPerPage = itemsPerPageElem != null ? int.Parse(itemsPerPageElem.Value) : -1;
                var items = feed.Elements(XName.Get("entry", Namespaces.AtomNS)).Select(e => Comment.Parse(e)).ToList();
                return new GoogleFeed<Comment>(icon, author, totalResults, startIndex2, itemsPerPage, items);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<Comment> AddCommentAsync(string albumId, string photoId, string comment)
        {
            var uri = GetPhotoUri2(photoId);
            var client = CreateClient();
            var entry = GetCommentXml(comment);
            var content = new StringContent(entry);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/atom+xml");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var xml = XDocument.Load(await response.Content.ReadAsStreamAsync());
                return Comment.Parse(xml.Root);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<Album> CreateAlbumAsync(string title, string summary, string location, string access, string timestamp, string keywords)
        {
            var uri = new Uri("https://picasaweb.google.com/data/feed/api/user/default", UriKind.Absolute);
            var client = CreateClient();
            var entry = GetAlbumXml("", title, summary, location, access, timestamp, keywords);
            var content = new StringContent(entry);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/atom+xml");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var xml = XDocument.Load(await response.Content.ReadAsStreamAsync());
                return Album.Parse(xml.Root);
            }
            else
            {
                throw await ProcessException(response.Content);
            }

        }

        public async Task<Stream> DownloadFileAsync(Uri uri, CancellationToken cancellationToken)
        {
            //var client = new HttpClient();
            var client = CreateClient();
            var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new StreamWithLength(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentLength);
            }
            else
            {

                throw await ProcessException(response.Content);
            }
        }

        public async Task<Photo> UploadFileAsync(string albumId, string title, string summary, string keywords, GeoPosition where, string contentType, Stream fileStream, IProgress<StreamProgress> progress, CancellationToken cancellationToken)
        {
            var uri = GetAlbumUri(albumId);
            var client = CreateClient();
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _oauth2Token);
            var content = new MultipartContent("related");
            var text = GetPhotoXml(title, summary, keywords, where);
            var textContent = new StringContent(text);
            textContent.Headers.ContentType = new MediaTypeHeaderValue("application/atom+xml");
            content.Add(textContent);
            var fileStreamContent = new StreamedContent(fileStream, progress, cancellationToken);
            fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "file", FileName = title };
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType ?? "image/jpeg");
            content.Add(fileStreamContent);
            var response = await client.PostAsync(uri, content, cancellationToken);//.AsTask(cancellationToken, progress);
            if (response.IsSuccessStatusCode)
            {
                var xml = XDocument.Load(await response.Content.ReadAsStreamAsync());
                return Photo.Parse(xml.Root);
            }
            else
            {

                throw await ProcessException(response.Content);
            }
        }

        public async Task<Album> UpdateAlbumAsync(string albumId, string etag, string title, string summary, string location, string access, string timestamp, string keywords)
        {
            var uri = GetAlbumUri2(albumId);
            var client = CreateClient();
            client.DefaultRequestHeaders.IfMatch.Add(string.IsNullOrWhiteSpace(etag) ? EntityTagHeaderValue.Any : new EntityTagHeaderValue(etag));
            var entry = GetAlbumXml(""/*albumId*/, title, summary, location, access, timestamp, keywords);
            var content = new StringContent(entry);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            var response = await client.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), uri));
            if (response.IsSuccessStatusCode)
            {
                var xml = XDocument.Load(await response.Content.ReadAsStreamAsync());
                return Album.Parse(xml.Root);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task<Photo> UpdateFileAsync(string fileId, string albumId, string etag, string title, string summary, string keywords, GeoPosition where)
        {
            var uri = GetPhotoUri(fileId, albumId);
            var client = CreateClient();
            client.DefaultRequestHeaders.IfMatch.Add(string.IsNullOrWhiteSpace(etag) ? EntityTagHeaderValue.Any : new EntityTagHeaderValue(etag));
            var entry = GetPhotoXml(title, summary, keywords, where);
            var content = new StringContent(entry);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            var response = await client.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), uri));
            if (response.IsSuccessStatusCode)
            {
                var xml = XDocument.Load(await response.Content.ReadAsStreamAsync());
                return Photo.Parse(xml.Root);
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task DeletePhotoAsync(string photoId, string etag)
        {
            var uri = GetPhotoUri(photoId);
            var client = CreateClient();
            client.DefaultRequestHeaders.IfMatch.Add(string.IsNullOrWhiteSpace(etag) ? EntityTagHeaderValue.Any : new EntityTagHeaderValue(etag));
            var response = await client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response.Content);
            }
        }

        public async Task DeleteAlbumAsync(string albumId, string etag)
        {
            var uri = GetAlbumUri2(albumId);
            var client = CreateClient();
            client.DefaultRequestHeaders.IfMatch.Add(string.IsNullOrWhiteSpace(etag) ? EntityTagHeaderValue.Any : new EntityTagHeaderValue(etag));
            var response = await client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response.Content);
            }
        }

        #endregion

        #region ** private stuff

        private Uri GetAlbumUri(string albumId)
        {
            return new Uri(string.Format("https://picasaweb.google.com/data/feed/api/user/default/albumid/{0}", albumId), UriKind.Absolute);
        }

        private Uri GetAlbumUri2(string albumId)
        {
            return new Uri(string.Format("https://picasaweb.google.com/data/entry/api/user/default/albumid/{0}", albumId), UriKind.Absolute);
        }

        private Uri GetPhotoUri(string photoId, string albumId = "default", string userId = "default")
        {
            return new Uri(string.Format("https://picasaweb.google.com/data/entry/api/user/{2}/albumid/{1}/photoid/{0}", photoId, albumId, userId), UriKind.Absolute);
        }

        private Uri GetPhotoUri2(string photoId)
        {
            return new Uri(string.Format("https://picasaweb.google.com/data/feed/api/user/default/albumid/default/photoid/{0}", photoId), UriKind.Absolute);
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("GData-Version", "2");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _oauth2Token);
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }

        private static string GetPhotoXml(string title, string summary, string keywords, GeoPosition where)
        {
            var text = @"<entry xmlns='http://www.w3.org/2005/Atom' 
                            xmlns:media='http://search.yahoo.com/mrss/'
                            xmlns:gml='http://www.opengis.net/gml'
                            xmlns:georss='http://www.georss.org/georss'>";
            text += @"      <title>" + new XText(title ?? "").ToString() + @"</title>
                            <summary>" + new XText(summary ?? "").ToString() + @"</summary>
                            <category scheme=""http://schemas.google.com/g/2005#kind""
                            term=""http://schemas.google.com/photos/2007#photo""/>
                            <media:group>
                                <media:keywords>" + new XText(keywords ?? "").ToString() + @"</media:keywords>
                            </media:group>";
            if (where != null)
            {
                text += @"      <georss:where>
                              <gml:Point>
                                <gml:pos>" + where.Latitude.ToString(CultureInfo.InvariantCulture) + " " + where.Longitude.ToString(CultureInfo.InvariantCulture) + @"</gml:pos>
                              </gml:Point>
                            </georss:where>";
            }
            text += @"      </entry>";
            return XDocument.Parse(text).ToString();
        }

        private static string GetCommentXml(string comment)
        {
            var text = @"<entry xmlns='http://www.w3.org/2005/Atom'>
                          <content>" + new XText(comment ?? "").ToString() + @"</content>
                          <category scheme='http://schemas.google.com/g/2005#kind'
                            term='http://schemas.google.com/photos/2007#comment'/>
                        </entry>";
            return XDocument.Parse(text).ToString();
        }

        private static string GetAlbumXml(string albumId, string title, string summary, string location, string access, string timestamp, string keywords)
        {
            var text = @"<entry xmlns='http://www.w3.org/2005/Atom' xmlns:media='http://search.yahoo.com/mrss/' xmlns:gphoto='http://schemas.google.com/photos/2007'>";
            if (!string.IsNullOrWhiteSpace(albumId))
            {
                text += "<id>" + albumId + "</id>";
            }
            if (title != null)
            {
                text += "<title type='text'>" + new XText(title ?? "").ToString() + "</title>";
            }
            if (summary != null)
            {
                text += "<summary type='text'>" + new XText(summary ?? "").ToString() + "</summary>";
            }
            if (location != null)
            {
                text += "<gphoto:location>" + new XText(location ?? "").ToString() + "</gphoto:location>";
            }
            if (access != null)
            {
                text += "<gphoto:access>" + access + "</gphoto:access>";
            }
            if (timestamp != null)
            {
                text += "<gphoto:timestamp>" + timestamp + "</gphoto:timestamp>";
            }
            if (keywords != null)
            {
                text += @"<media:group><media:keywords>" + new XText(keywords ?? "").ToString() + "</media:keywords></media:group>";
            }
            text += @"<category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/photos/2007#album'></category>";
            text += "</entry>";
            return XDocument.Parse(text).ToString();
        }

        private async Task<Exception> ProcessException(HttpContent httpContent)
        {
            var contentText = await httpContent.ReadAsStringAsync();
            return new Exception(contentText);
        }

        public static Uri BuildUri(string uri, string q = null, string kind = null, string fields = null, int? startIndex = null, int? maxResults = null, string orderby = null)
        {
            var builder = new UriBuilder(uri);
            var query = builder.Query ?? "";
            if (query.StartsWith("?"))
                query = query.Substring(1);
            if (orderby != null)
                query += "&orderby=" + orderby;
            if (startIndex.HasValue && startIndex > 0)
                query += "&start-index=" + startIndex;
            if (maxResults.HasValue && maxResults > 0)
                query += "&max-results=" + maxResults;
            if (!string.IsNullOrWhiteSpace(q))
                query += "&q=" + Uri.EscapeDataString(q);
            if (!string.IsNullOrWhiteSpace(kind))
                query += "&kind=" + kind;
            if (!string.IsNullOrWhiteSpace(fields))
                query += "&fields=" + Uri.EscapeDataString(fields);
            builder.Query = query;
            return builder.Uri;
        }

        #endregion
    }
}
