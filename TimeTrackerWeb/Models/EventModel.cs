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

        private DateTime _startDateTime;

        [Required]
        [BsonElement("startdatetime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start", Description = "The Start Time and Date")]
        public DateTime StartDateTime
        {
            get { return _startDateTime.ToLocalTime(); }
            set { _startDateTime = value.ToUniversalTime(); }
        }

        private DateTime? _endDateTime;

        [BsonElement("enddatetime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "End", Description = "The End Time and Date")]
        public DateTime? EndDateTime
        {
            get { return _endDateTime?.ToLocalTime(); }
            set { _endDateTime = value?.ToUniversalTime(); }
        }

        [BsonElement("description")]
        [MinLength(3)]
        public string Description { get; set; }



        // ****** Display Only Fields ******
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public string StartTimeDisplayOnly
        {
            get { return _startDateTime.ToLocalTime().ToShortTimeString(); }
        }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public string? EndTimeDisplayOnly
        {
            get { return _endDateTime?.ToLocalTime().ToShortTimeString(); }
        }
    }
}