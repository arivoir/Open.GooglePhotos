using System.Runtime.Serialization;

namespace Open.GooglePhotos
{
    /// <summary>
    /// Filters that can be applied to a media item search. If multiple filter options are specified, they're treated as AND with each other.
    /// </summary>
    [DataContract]
    public class Filters
    {
        /// <summary>
        /// Filters the media items based on their creation date. 
        /// </summary>
        [DataMember(Name = "dateFilter")]
        public DateFilter DateFilter { get; set; }

        /// <summary>
        /// Filters the media items based on their content. 
        /// </summary>
        [DataMember(Name = "contentFilter")]
        public ContentFilter ContentFilter { get; set; }

        /// <summary>
        /// Filters the media items based on the type of media. 
        /// </summary>
        [DataMember(Name = "mediaTypeFilter")]
        public MediaTypeFilter MediaTypeFilter { get; set; }

        /// <summary>
        /// If set, the results include media items that the user has archived. Defaults to false (archived media items aren't included).
        /// </summary>
        [DataMember(Name = "includeArchivedMedia")]
        public bool IncludeArchivedMedia { get; set; }

        /// <summary>
        /// If set, the results exclude media items that were not created by this app. Defaults to false (all media items are returned). This field is ignored if the photoslibrary.readonly.appcreateddata scope is used.
        /// </summary>
        [DataMember(Name = "excludeNonAppCreatedData")]
        public bool ExcludeNonAppCreatedData { get; set; }
    }

    /// <summary>
    /// This filter defines the allowed dates or date ranges for the media returned. It's possible to pick a set of specific dates and a set of date ranges.
    /// </summary>
    [DataContract]
    public class DateFilter
    {
        /// <summary>
        /// List of dates that match the media items' creation date. A maximum of 5 dates can be included per request. 
        /// </summary>
        [DataMember(Name = "dates")]
        public Date[] Dates { get; set; }

        /// <summary>
        /// List of dates ranges that match the media items' creation date. A maximum of 5 dates ranges can be included per request. 
        /// </summary>
        [DataMember(Name = "ranges")]
        public DateRange[] Ranges { get; set; }
    }

    /// <summary>
    /// Represents a whole calendar date. The day may be 0 to represent a year and month where the day isn't significant, such as a whole calendar month. The month may be 0 to represent a a day and a year where the month isn't signficant, like when you want to specify the same day in every month of a year or a specific year. The year may be 0 to represent a month and day independent of year, like an anniversary date.
    /// </summary>
    [DataContract]
    public class Date
    {
        /// <summary>
        /// Year of date. Must be from 1 to 9999, or 0 if specifying a date without a year. 
        /// </summary>
        [DataMember(Name = "year")]
        public int Year { get; set; }

        /// <summary>
        /// Month of year. Must be from 1 to 12, or 0 if specifying a year without a month and day. 
        /// </summary>
        [DataMember(Name = "month")]
        public int Month { get; set; }

        /// <summary>
        /// Day of month. Must be from 1 to 31 and valid for the year and month, or 0 if specifying a year/month where the day isn't significant. 
        /// </summary>
        [DataMember(Name = "day")]
        public int Day { get; set; }
    }

    /// <summary>
    /// Defines a range of dates. Both dates must be of the same format. For more information, see <see cref="Date"/>.
    /// </summary>
    [DataContract]
    public class DateRange
    {
        /// <summary>
        /// The start date (included as part of the range) in one of the formats described.  
        /// </summary>
        [DataMember(Name = "startDate")]
        public Date StartDate { get; set; }

        /// <summary>
        /// The end date (included as part of the range). It must be specified in the same format as the start date. 
        /// </summary>
        [DataMember(Name = "endDate")]
        public Date EndDate { get; set; }
    }

    /// <summary>
    /// This filter allows you to return media items based on the content type.
    /// </summary>
    /// <remarks>
    /// It's possible to specify a list of categories to include, and/or a list of categories to exclude. Within each list, the categories are combined with an OR. 
    /// The content filter includedContentCategories: [c1, c2, c3] would get media items that contain(c1 OR c2 OR c3). 
    /// The content filter excludedContentCategories: [c1, c2, c3] would NOT get media items that contain(c1 OR c2 OR c3). 
    /// You can also include some categories while excluding others, as in this example: includedContentCategories: [c1, c2], excludedContentCategories: [c3, c4]
    /// The previous example would get media items that contain (c1 OR c2) AND NOT (c3 OR c4). A category that appears in includedContentategories must not appear in excludedContentCategories.
    /// </remarks>
    [DataContract]
    public class ContentFilter
    {
        /// <summary>
        /// The set of categories to be included in the media item search results. The items in the set are ORed. There's a maximum of 10 includedContentCategories per request.
        /// </summary>
        [DataMember(Name = "includedContentCategories")]
        public ContentCategory[] IncludedContentCategories { get; set; }

        /// <summary>
        /// The set of categories which are not to be included in the media item search results. The items in the set are ORed. There's a maximum of 10 excludedContentCategories per request. 
        /// </summary>
        [DataMember(Name = "excludedContentCategories")]
        public ContentCategory[] ExcludedContentCategories { get; set; }
    }

    public enum ContentCategory
    {
        /// <summary>
        /// Default content category. This category is ignored when any other category is used in the filter.
        /// </summary>
        NONE,
        /// <summary>
        /// Media items containing landscapes.
        /// </summary>
        LANDSCAPES,
        /// <summary>
        /// Media items containing receipts.
        /// </summary>
        RECEIPTS,
        /// <summary>
        /// Media items containing cityscapes.
        /// </summary>
        CITYSCAPES,
        /// <summary>
        /// Media items containing landmarks.
        /// </summary>
        LANDMARKS,
        /// <summary>
        /// Media items that are selfies.
        /// </summary>
        SELFIES,
        /// <summary>
        /// Media items containing people.
        /// </summary>
        PEOPLE,
        /// <summary>
        /// Media items containing pets.
        /// </summary>
        PETS,
        /// <summary>
        /// Media items from weddings.
        /// </summary>
        WEDDINGS,
        /// <summary>
        /// Media items from birthdays.
        /// </summary>
        BIRTHDAYS,
        /// <summary>
        /// Media items containing documents.
        /// </summary>
        DOCUMENTS,
        /// <summary>
        /// Media items taken during travel.
        /// </summary>
        TRAVEL,
        /// <summary>
        /// Media items containing animals.
        /// </summary>
        ANIMALS,
        /// <summary>
        /// Media items containing food.
        /// </summary>
        FOOD,
        /// <summary>
        /// Media items from sporting events.
        /// </summary>
        SPORT,
        /// <summary>
        /// Media items taken at night.
        /// </summary>
        NIGHT,
        /// <summary>
        /// Media items from performances.
        /// </summary>
        PERFORMANCES,
        /// <summary>
        /// Media items containing whiteboards.
        /// </summary>
        WHITEBOARDS,
        /// <summary>
        /// Media items that are screenshots.
        /// </summary>
        SCREENSHOTS,
        /// <summary>
        /// Media items that are considered to be utility. These include, but aren't limited to documents, screenshots, whiteboards etc.
        /// </summary>
        UTILITY,
    }

    /// <summary>
    /// This filter defines the type of media items to be returned, for example, videos or photos. All the specified media types are treated as an OR when used together.
    /// </summary>
    [DataContract]
    public class MediaTypeFilter
    {
        /// <summary>
        /// The types of media items to be included. This field should be populated with only one media type. If you specify multiple media types, it results in an error. 
        /// </summary>
        [DataMember(Name = "mediaTypes")]
        public MediaType[] MediaTypes { get; set; }
    }

    public enum MediaType
    {
        /// <summary>
        /// Treated as if no filters are applied. All media types are included.
        /// </summary>
        ALL_MEDIA,
        /// <summary>
        /// All media items that are considered videos. This also includes movies the user has created using the Google Photos app.
        /// </summary>
        VIDEO,
        /// <summary>
        /// All media items that are considered photos.This includes.bmp, .gif, .ico, .jpg (and other spellings), .tiff, .webp and special photo types such as iOS live photos, Android motion photos, panoramas, photospheres.
        /// </summary>
        PHOTO,
    }
}
