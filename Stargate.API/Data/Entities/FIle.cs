using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Stargate.API.Data.Entities
{
    public class File
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FileName { get; set; }
            public string ContentType { get; set; }
            public string ShortUri { get; set; }
            public string AbsoluteUri { get; set; }
        
            public string ExternalUri { get; set; }
            public string FileExtension { get; set; }
            public long FileSizeBytes { get; set; }
            public DateTime CreatedAtUtc { get; set; }
    }
}