using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TimeTrackerWeb.Models.Base;

namespace TimeTrackerWeb.Models
{
    public class EventModel : EntityBase
    {
        [Required]
        [BsonElement("projectid")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "Project ID", Prompt = "5eb9eb10a0e5812c7caa399f",
            Description = "The unique project ID attaching the record to the correct project")]
        public string ProjectId { get; set; }

        [Required]
        [BsonElement("starttime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime StartTime { get; set; }

        [Required]
        [BsonElement("startdate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [BsonElement("endtime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime? EndTime { get; set; }

        [BsonElement("enddate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [BsonElement("description")]
        [MinLength(3)]
        public string Description { get; set; }
    }
}