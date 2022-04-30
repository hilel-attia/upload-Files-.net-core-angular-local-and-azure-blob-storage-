using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stargate.API.ViewModels
{
    public class UploadResponseViewModel
    {
       [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public bool Success { get; set; }
        public string FileName { get; set; }
        public string Uri { get; set; }
    }
}