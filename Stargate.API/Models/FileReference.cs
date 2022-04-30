using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stargate.API.Models
{
    public class FileReference
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Uri { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}