using System.Runtime.Serialization;

namespace Open.GooglePhotos
{
    /// <summary>
    /// Creates an album in a user's Google Photos library.
    /// </summary>
    [DataContract]
    public class AlbumRequest
    {
        /// <summary>
        /// The album to be created. 
        /// </summary>
        [DataMember(Name = "album")]
        public Album Album { get; set; }
    }

    /// <summary>
    /// List of albums requested.
    /// </summary>
    [DataContract]
    public class AlbumsResponse
    {
        /// <summary>
        /// List of albums shown in the Albums tab of the user's Google Photos app. 
        /// </summary>
        [DataMember(Name = "albums")]
        public Album[] Albums { get; set; }

        /// <summary>
        /// Token to use to get the next set of albums. Populated if there are more albums to retrieve for this request. 
        /// </summary>
        [DataMember(Name = "nextPageToken")]
        public string NextPageToken { get; set; }
    }

    /// <summary>
    /// Representation of an album in Google Photos. Albums are containers for media items. If an album has been shared by the application, it contains an extra shareInfo property.
    /// </summary>
    [DataContract]
    public class Album
    {
        /// <summary>
        /// Identifier for the album. This is a persistent identifier that can be used between sessions to identify this album. 
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Name of the album displayed to the user in their Google Photos account. This string shouldn't be more than 500 characters. 
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Google Photos URL for the album. The user needs to be signed in to their Google Photos account to access this link. 
        /// </summary>
        [DataMember(Name = "productUrl", EmitDefaultValue = false)]
        public string ProductUrl { get; set; }

        /// <summary>
        /// True if you can create media items in this album. This field is based on the scopes granted and permissions of the album. If the scopes are changed or permissions of the album are changed, this field is updated. 
        /// </summary>
        [DataMember(Name = "isWriteable", EmitDefaultValue = false)]
        public bool IsWriteable { get; set; }

        /// <summary>
        /// Information related to shared albums. This field is only populated if the album is a shared album, the developer created the album and the user has granted the photoslibrary.sharing scope. 
        /// </summary>
        [DataMember(Name = "shareInfo", EmitDefaultValue = false)]
        public ShareInfo ShareInfo { get; set; }

        /// <summary>
        /// The number of media items in the album. 
        /// </summary>
        [DataMember(Name = "mediaItemsCount", EmitDefaultValue = false)]
        public long MediaItemsCount { get; set; }

        /// <summary>
        /// A URL to the cover photo's bytes. This shouldn't be used as is. Parameters should be appended to this URL before use. For example, '=w2048-h1024' sets the dimensions of the cover photo to have a width of 2048 px and height of 1024 px. 
        /// </summary>
        [DataMember(Name = "coverPhotoBaseUrl", EmitDefaultValue = false)]
        public string CoverPhotoBaseUrl { get; set; }

        /// <summary>
        /// Identifier for the media item associated with the cover photo.
        /// </summary>
        [DataMember(Name = "coverPhotoMediaItemId", EmitDefaultValue = false)]
        public string CoverPhotoMediaItemId { get; set; }
    }

    /// <summary>
    /// Information about albums that are shared. This information is only included if you created the album, it is shared and you have the sharing scope.
    /// </summary>
    [DataContract]
    public class ShareInfo
    {
        /// <summary>
        /// Options that control the sharing of an album. 
        /// </summary>
        [DataMember(Name = "sharedAlbumOptions")]
        public SharedAlbumOptions sharedAlbumOptions { get; set; }

        /// <summary>
        /// A link to the album that's now shared on the Google Photos website and app. Anyone with the link can access this shared album and see all of the items present in the album. 
        /// </summary>
        [DataMember(Name = "shareableUrl")]
        public string ShareableUrl { get; set; }

        /// <summary>
        /// A token that can be used by other users to join this shared album via the API. 
        /// </summary>
        [DataMember(Name = "shareToken")]
        public string ShareToken { get; set; }

        /// <summary>
        /// True if the user has joined the album. This is always true for the owner of the shared album. 
        /// </summary>
        [DataMember(Name = "isJoined")]
        public bool IsJoined { get; set; }

    }

    /// <summary>
    /// Options that control the sharing of an album.
    /// </summary>
    [DataContract]
    public class SharedAlbumOptions
    {
        /// <summary>
        /// True if the shared album allows collaborators (users who have joined the album) to add media items to it. Defaults to false. 
        /// </summary>
        [DataMember(Name = "isCollaborative")]
        public bool isCollaborative { get; set; }

        /// <summary>
        /// True if the shared album allows the owner and the collaborators (users who have joined the album) to add comments to the album. Defaults to false. 
        /// </summary>
        [DataMember(Name = "isCommentable")]
        public bool IsCommentable { get; set; }
    }

    /// <summary>
    /// Specifies a position in an album.
    /// </summary>
    [DataContract]
    public class AlbumPosition
    {
        /// <summary>
        /// Type of position, for a media or enrichment item. 
        /// </summary>
        [DataMember(Name = "position")]
        public PositionType Position { get; set; }

        /// <summary>
        /// The media item to which the position is relative to. Only used when position type is AFTER_MEDIA_ITEM. 
        /// </summary>
        [DataMember(Name = "relativeMediaItemId")]
        public string RelativeMediaItemId { get; set; }

        /// <summary>
        /// The enrichment item to which the position is relative to. Only used when position type is AFTER_ENRICHMENT_ITEM. 
        /// </summary>
        [DataMember(Name = "relativeEnrichmentItemId")]
        public string RelativeEnrichmentItemId { get; set; }
    }

    public enum PositionType
    {
        /// <summary>
        /// Default value if this enum isn't set.
        /// </summary>
        POSITION_TYPE_UNSPECIFIED,
        /// <summary>
        /// At the beginning of the album.
        /// </summary>
        FIRST_IN_ALBUM,
        /// <summary>
        /// At the end of the album.
        /// </summary>
        LAST_IN_ALBUM,
        /// <summary>
        /// After a media item.
        /// </summary>
        AFTER_MEDIA_ITEM,
        /// <summary>
        /// After an enrichment item.
        /// </summary>
        AFTER_ENRICHMENT_ITEM,
    }
}
