using Open.Google;
using Open.IO;
using Open.Net.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Open.GooglePhotos
{
    public class GooglePhotosClient : GoogleClient
    {
        #region ** fields

        private static readonly string ApiServiceUri = "https://photoslibrary.googleapis.com/v1/";
        public const string LIBRARY_SCOPE = "https://www.googleapis.com/auth/photoslibrary";
        public const string READ_ONLY_SCOPE = "https://www.googleapis.com/auth/photoslibrary.readonly";
        public const string APPEND_ONLY_SCOPE = "https://www.googleapis.com/auth/photoslibrary.appendonly";
        public const string APP_CREATED_DATA_SCOPE = "https://www.googleapis.com/auth/photoslibrary.readonly.appcreateddata";
        public const string SHARING_SCOPE = "https://www.googleapis.com/auth/photoslibrary.sharing";
        private string _oauth2Token;

        #endregion

        #region ** initialization

        public GooglePhotosClient(string oauth2Token)
        {
            _oauth2Token = oauth2Token;
        }

        #endregion

        #region ** authentication

        public static new string GetRequestUrl(string clientId, string scope, string callbackUrl)
        {
            return GoogleClient.GetRequestUrl(clientId, scope, callbackUrl);
        }

        #endregion

        #region ** public methods

        public async Task<AlbumsResponse> GetAlbumsAsync(string pageToken = null, int? pageSize = null, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = BuildUri(ApiServiceUri + "albums", pageToken: pageToken, fields: fields, pageSize: pageSize);
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<AlbumsResponse>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<SearchResponse> SearchAsync(string albumId = null, int? pageSize = null, string pageToken = null, Filters filters = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = BuildUri(ApiServiceUri + "mediaItems:search");
            var client = CreateClient();
            var searchRequest = new SearchRequest();
            searchRequest.AlbumId = albumId;
            searchRequest.PageSize = pageSize;
            searchRequest.PageToken = pageToken;
            searchRequest.Filters = filters;
            var content = new StringContent(searchRequest.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<SearchResponse>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Album> CreateAlbumAsync(string title, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = BuildUri(ApiServiceUri + "albums");
            var client = CreateClient();
            var album = new Album();
            album.Title = title;
            var albumRequest = new AlbumRequest();
            albumRequest.Album = album;
            var content = new StringContent(albumRequest.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Album>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<NewMediaItemsResponse> CreateMediaItemAsync(string uploadToken, string albumId, string description, AlbumPosition albumPosition, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = BuildUri(ApiServiceUri + "mediaItems:batchCreate");
            var client = CreateClient();
            var newMediaItem = new NewMediaItem();
            newMediaItem.Description = description;
            newMediaItem.SimpleMediaItem = new SimpleMediaItem { UploadToken = uploadToken };
            var mediaItemRequest = new MediaItemRequest();
            mediaItemRequest.AlbumId = albumId;
            mediaItemRequest.NewMediaItems = new NewMediaItem[] { newMediaItem };
            mediaItemRequest.AlbumPosition = albumPosition;
            var content = new StringContent(mediaItemRequest.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<NewMediaItemsResponse>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Stream> DownloadFileAsync(Uri uri, CancellationToken cancellationToken)
        {
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

        public async Task<(Uri url, long chunk)> InitiateUploadSessionAsync(string mimeType, string fileName, long fileLength, CancellationToken cancellationToken)
        {
            var uri = BuildUri(ApiServiceUri + "uploads");
            var client = CreateClient();
            client.DefaultRequestHeaders.Add("X-Goog-Upload-Command", "start");
            client.DefaultRequestHeaders.Add("X-Goog-Upload-Content-Type", mimeType);
            client.DefaultRequestHeaders.Add("X-Goog-Upload-File-Name", fileName);
            client.DefaultRequestHeaders.Add("X-Goog-Upload-Protocol", "resumable");
            client.DefaultRequestHeaders.Add("X-Goog-Upload-Raw-Size", fileLength.ToString());
            var content = new StringContent("");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var url = new Uri(response.Headers.GetValues("X-Goog-Upload-URL").First());
                var chunk = long.Parse(response.Headers.GetValues("X-Goog-Upload-Chunk-Granularity").First());
                return (url, chunk);
            }
            else
            {
                throw await ProcessException(response);
            }
        }


        public async Task<string> UploadResumableFileAsync(Uri sessionUri, Stream fileStream, IProgress<StreamProgress> progress, CancellationToken cancellationToken)
        {
            var client = CreateClient();
            client.DefaultRequestHeaders.Add("X-Goog-Upload-Command", "upload, finalize");
            client.DefaultRequestHeaders.Add("X-Goog-Upload-Offset", "0");
            var content = new StreamedContent(fileStream, progress, cancellationToken);
            var response = await client.PutAsync(sessionUri, content, cancellationToken);
            return await response.Content.ReadAsStringAsync();
        }

        #endregion

        #region ** private stuff

        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("GData-Version", "2");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _oauth2Token);
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }

        private async Task<Exception> ProcessException(HttpContent httpContent)
        {
            var contentText = await httpContent.ReadAsStringAsync();
            return new Exception(contentText);
        }
        private async Task<Exception> ProcessException(HttpResponseMessage response)
        {
            var contentText = await response.Content.ReadAsStringAsync();
            return new Exception(contentText);
        }

        public static Uri BuildUri(string uri, string fields = null, string pageToken = null, int? pageSize = null)
        {
            var builder = new UriBuilder(uri);
            var query = builder.Query ?? "";
            if (query.StartsWith("?"))
                query = query.Substring(1);
            if (!string.IsNullOrWhiteSpace(pageToken))
                query += "&pageToken=" + pageToken;
            if (pageSize.HasValue && pageSize > 0)
                query += "&pageSize=" + pageSize;
            if (!string.IsNullOrWhiteSpace(fields))
                query += "&fields=" + Uri.EscapeDataString(fields);
            builder.Query = query;
            return builder.Uri;
        }

        #endregion
    }
}
