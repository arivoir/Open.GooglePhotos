using System.Runtime.Serialization;

namespace Open.GooglePhotos
{
    /// <summary>
    /// The Status type defines a logical error model that is suitable for different programming environments, including REST APIs and RPC APIs.
    /// </summary>
    [DataContract]
    public class Status
    {
        /// <summary>
        /// The status code, which should be an enum value of google.rpc.Code. 
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// A developer-facing error message, which should be in English. Any user-facing error message should be localized and sent in the google.rpc.Status.details field, or localized by the client. 
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// A list of messages that carry the error details. There is a common set of message types for APIs to use.
        /// An object containing fields of an arbitrary type.An additional field "@type" contains a URI identifying the type.Example: { "id": 1234, "@type": "types.example.com/standard/id" }.
        /// </summary>
        [DataMember(Name = "details")]
        public string Details { get; set; }
    }
}
