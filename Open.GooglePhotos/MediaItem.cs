using System.Runtime.Serialization;

namespace Open.GooglePhotos
{
    /// <summary>
    /// Creates an album in a user's Google Photos library.
    /// </summary>
    [DataContract]
    public class MediaItemRequest
    {
        /// <summary>
        /// Identifier of the album where the media items are added. The media items are also added to the user's library. This is an optional field. 
        /// </summary>
        [DataMember(Name = "albumId", EmitDefaultValue = false)]
        public string AlbumId { get; set; }

        /// <summary>
        /// List of media items to be created. 
        /// </summary>
        [DataMember(Name = "newMediaItems")]
        public NewMediaItem[] NewMediaItems { get; set; }

        /// <summary>
        /// Position in the album where the media items are added. If not specified, the media items are added to the end of the album (as per the default value, that is, LAST_IN_ALBUM). The request fails if this field is set and the albumId is not specified. The request will also fail if you set the field and are not the owner of the shared album. 
        /// </summary>
        [DataMember(Name = "albumPosition", EmitDefaultValue = false)]
        public AlbumPosition AlbumPosition { get; set; }
    }

    /// <summary>
    /// New media item that's created in a user's Google Photos account.
    /// </summary>
    [DataContract]
    public class NewMediaItem
    {
        /// <summary>
        /// Description of the media item. This will be shown to the user in the item's info section in the Google Photos app. This string shouldn't be more than 1000 characters. 
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// A new media item that has been uploaded via the included uploadToken. 
        /// </summary>
        [DataMember(Name = "simpleMediaItem")]
        public SimpleMediaItem SimpleMediaItem { get; set; }
    }

    /// <summary>
    /// A simple media item to be created in Google Photos via an upload token.
    /// </summary>
    [DataContract]
    public class SimpleMediaItem
    {
        /// <summary>
        /// Token identifying the media bytes that have been uploaded to Google. 
        /// </summary>
        [DataMember(Name = "uploadToken")]
        public string UploadToken { get; set; }
    }

    /// <summary>
    /// List of media items created.
    /// </summary>
    [DataContract]
    public class NewMediaItemsResponse
    {
        /// <summary>
        /// List of media items created. 
        /// </summary>
        [DataMember(Name = "newMediaItemResults")]
        public NewMediaItemResult[] NewMediaItemResults { get; set; }
    }

    /// <summary>
    /// Result of creating a new media item.
    /// </summary>
    [DataContract]
    public class NewMediaItemResult
    {
        /// <summary>
        /// The upload token used to create this new media item.
        /// </summary>
        [DataMember(Name = "uploadToken")]
        public string UploadToken { get; set; }

        /// <summary>
        /// If an error occurred during the creation of this media item, this field is populated with information related to the error. For details regarding this field, see Status. 
        /// </summary>
        [DataMember(Name = "status")]
        public Status Status { get; set; }

        /// <summary>
        /// Media item created with the upload token. It's populated if no errors occurred and the media item was created successfully. 
        /// </summary>
        [DataMember(Name = "mediaItem")]
        public MediaItem MediaItem { get; set; }
    }

    /// <summary>
    /// Representation of a media item (such as a photo or video) in Google Photos.
    /// </summary>
    [DataContract]
    public class MediaItem
    {
        /// <summary>
        /// Identifier for the media item. This is a persistent identifier that can be used between sessions to identify this media item. 
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Description of the media item. This is shown to the user in the item's info section in the Google Photos app. 
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Google Photos URL for the media item. This link is available to the user only if they're signed in. 
        /// </summary>
        [DataMember(Name = "productUrl")]
        public string ProductUrl { get; set; }

        /// <summary>
        /// A URL to the media item's bytes. This shouldn't be used directly to access the media item. For example, '=w2048-h1024' will set the dimensions of a media item of type photo to have a width of 2048 px and height of 1024 px. 
        /// </summary>
        [DataMember(Name = "baseUrl")]
        public string BaseUrl { get; set; }

        /// <summary>
        /// MIME type of the media item. For example, image/jpeg. 
        /// </summary>
        [DataMember(Name = "mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// Metadata related to the media item, such as, height, width, or creation time. 
        /// </summary>
        [DataMember(Name = "mediaMetadata")]
        public MediaMetadata MediaMetadata { get; set; }

        /// <summary>
        /// Information about the user who created this media item. 
        /// </summary>
        [DataMember(Name = "contributorInfo")]
        public ContributorInfo ContributorInfo { get; set; }

        /// <summary>
        /// Filename of the media item. This is shown to the user in the item's info section in the Google Photos app. 
        /// </summary>
        [DataMember(Name = "filename")]
        public string Filename { get; set; }
    }

    /// <summary>
    /// Metadata for a media item.
    /// </summary>
    [DataContract]
    public class MediaMetadata
    {
        /// <summary>
        /// Time when the media item was first created (not when it was uploaded to Google Photos).
        /// A timestamp in RFC3339 UTC "Zulu" format, accurate to nanoseconds.Example: "2014-10-02T15:01:23.045123456Z".
        /// </summary>
        [DataMember(Name = "creationTime")]
        public string CreationTime { get; set; }

        /// <summary>
        /// Original width (in pixels) of the media item. 
        /// </summary>
        [DataMember(Name = "width")]
        public long Width { get; set; }

        /// <summary>
        /// Original height (in pixels) of the media item. 
        /// </summary>
        [DataMember(Name = "height")]
        public long Height { get; set; }

        /// <summary>
        /// Metadata for a photo media type. 
        /// </summary>
        [DataMember(Name = "photo")]
        public Photo Photo { get; set; }

        /// <summary>
        /// Metadata for a video media type. 
        /// </summary>
        [DataMember(Name = "video")]
        public Video Video { get; set; }
    }

    /// <summary>
    /// Metadata that is specific to a photo, such as, ISO, focal length and exposure time. Some of these fields may be null or not included.
    /// </summary>
    [DataContract]
    public class Photo
    {
        /// <summary>
        /// Brand of the camera with which the photo was taken. 
        /// </summary>
        [DataMember(Name = "cameraMake")]
        public string CameraMake { get; set; }

        /// <summary>
        /// Model of the camera with which the photo was taken. 
        /// </summary>
        [DataMember(Name = "cameraModel")]
        public string CameraModel { get; set; }

        /// <summary>
        /// Focal length of the camera lens with which the photo was taken. 
        /// </summary>
        [DataMember(Name = "focalLength")]
        public double FocalLength { get; set; }

        /// <summary>
        /// Aperture f number of the camera lens with which the photo was taken. 
        /// </summary>
        [DataMember(Name = "apertureFNumber")]
        public double ApertureFNumber { get; set; }

        /// <summary>
        /// ISO of the camera with which the photo was taken. 
        /// </summary>
        [DataMember(Name = "isoEquivalent")]
        public int IsoEquivalent { get; set; }

        /// <summary>
        /// Exposure time of the camera aperture when the photo was taken.
        /// A duration in seconds with up to nine fractional digits, terminated by 's'. Example: "3.5s".
        /// </summary>
        [DataMember(Name = "exposureTime")]
        public string ExposureTime { get; set; }
    }

    /// <summary>
    /// Metadata that is specific to a video, for example, fps and processing status. Some of these fields may be null or not included.
    /// </summary>
    [DataContract]
    public class Video
    {
        /// <summary>
        /// Brand of the camera with which the video was taken. 
        /// </summary>
        [DataMember(Name = "cameraMake")]
        public string CameraMake { get; set; }

        /// <summary>
        /// Model of the camera with which the video was taken.  
        /// </summary>
        [DataMember(Name = "cameraModel")]
        public string CameraModel { get; set; }

        /// <summary>
        /// Frame rate of the video. 
        /// </summary>
        [DataMember(Name = "fps")]
        public double FPS { get; set; }

        /// <summary>
        /// Processing status of the video. 
        /// </summary>
        [DataMember(Name = "status")]
        public VideoProcessingStatus Status { get; set; }
    }

    public enum VideoProcessingStatus
    {
        /// <summary>
        /// Video processing status is unknown.
        /// </summary>
        UNSPECIFIED,
        /// <summary>
        /// Video is being processed. The user sees an icon for this video in the Google Photos app; however, it isn't playable yet.
        /// </summary>
        PROCESSING,
        /// <summary>
        /// Video processing is complete and it is now ready for viewing.
        /// </summary>
        READY,
        /// <summary>
        /// Something has gone wrong and the video has failed to process.
        /// </summary>
        FAILED,
    }

    /// <summary>
    /// Information about the user who added the media item. Note that this information is included only if the media item is within a shared album created by your app and you have the sharing scope.
    /// </summary>
    [DataContract]
    public class ContributorInfo
    {
        /// <summary>
        /// URL to the profile picture of the contributor. 
        /// </summary>
        [DataMember(Name = "profilePictureBaseUrl")]
        public string ProfilePictureBaseUrl { get; set; }

        /// <summary>
        /// Display name of the contributor. 
        /// </summary>
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
    }
}
