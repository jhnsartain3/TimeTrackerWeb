using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using TimeTrackerWeb.Models.Base;

namespace TimeTrackerWeb.Models
{
    public class UserModel : EntityBase
    {
        [BsonElement("username")]
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [BsonElement("password")]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}