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

        private DateTime _startTime;

        [Required]
        [BsonElement("starttime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime StartTime
        {
            get { return _startTime.ToLocalTime(); }
            set { _startTime = value.ToUniversalTime(); }
        }

        [Required]
        [BsonElement("startdate")]
        [DataType(DataType.Date)]
        [BsonDateTimeOptions(DateOnly = true)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        private DateTime? _endTime;

        [BsonElement("endtime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime? EndTime
        {
            get { return _endTime?.ToLocalTime(); }
            set { _endTime = value?.ToUniversalTime(); }
        }

        [BsonElement("enddate")]
        [DataType(DataType.Date)]
        [BsonDateTimeOptions(DateOnly = true)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EndDate { get; set; }

        [BsonElement("description")]
        [MinLength(3)]
        public string Description { get; set; }
    }
}