using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TimeTrackerWeb.Models.Base
{
    public abstract class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Record ID", Prompt = "5eb9eb10a0e5812c7caa399f",
            Description = "The unique identifying the record")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userid")]
        [Display(Name = "User ID", Prompt = "5eb9eb10a0e5812c7caa399f",
            Description = "The unique user ID identifying the record")]
        public string? UserId { get; set; }
    }
}