using System.Runtime.Serialization;

namespace Open.GooglePhotos
{
    /// <summary>
    /// Searches for media items in a user's Google Photos library. 
    /// </summary>
    /// <remarks>
    /// If no filters are set, then all media items in the user's library are returned. If an album is set, all media items in the specified album are returned. If filters are specified, media items that match the filters from the user's library are listed. If you set both the album and the filters, the request results in an error.
    /// </remarks>
    [DataContract]
    public class SearchRequest
    {
        /// <summary>
        /// Identifier of an album. If populated, lists all media items in specified album. Can't set in conjunction with any filters. 
        /// </summary>
        [DataMember(Name = "albumId", EmitDefaultValue = false)]
        public string AlbumId { get; set; }

        /// <summary>
        /// Maximum number of media items to return in the response. The default number of media items to return at a time is 25. The maximum pageSize is 100. 
        /// </summary>
        [DataMember(Name = "pageSize", EmitDefaultValue = false)]
        public int? PageSize { get; set; }

        /// <summary>
        /// A continuation token to get the next page of the results. Adding this to the request returns the rows after the pageToken. The pageToken should be the value returned in the nextPageToken parameter in the response to the searchMediaItems request. 
        /// </summary>
        [DataMember(Name = "pageToken", EmitDefaultValue = false)]
        public string PageToken { get; set; }

        /// <summary>
        /// Filters to apply to the request. Can't be set in conjunction with an albumId. 
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public Filters Filters { get; set; }
    }

    /// <summary>
    /// List of media items that match the search parameters.
    /// </summary>
    [DataContract]
    public class SearchResponse
    {
        /// <summary>
        /// Use this token to get the next set of media items. Its presence is the only reliable indicator of more media items being available in the next request. 
        /// </summary>
        [DataMember(Name = "nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>
        /// List of media items that match the search parameters. 
        /// </summary>
        [DataMember(Name = "mediaItems")]
        public MediaItem[] MediaItems { get; set; }
    }

    
}
