using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TimeTrackerWeb.Models.Base;
using TimeTrackerWeb.Models.Time;

namespace TimeTrackerWeb.Models
{
    public class EventModelWithQuantitiesOfTimeAndDateTimeInfoHeaders : EventModel
    {
        public EventModelWithQuantitiesOfTimeAndDateTimeInfoHeaders()
        {
        }

        public EventModelWithQuantitiesOfTimeAndDateTimeInfoHeaders(EventModel eventModel)
        {
            Description = eventModel.Description;
            ProjectId = eventModel.ProjectId;
            StartDateTime = eventModel.StartDateTime;
            EndDateTime = eventModel.EndDateTime;
            Id = eventModel.Id;
            UserId = eventModel.UserId;
        }

        public QuantitiesOfTimeModel QuantitiesOfTimeModel { get; set; }

        public bool IsDateTimeInfoHeaderRecord => StartDateTime.ToString().Equals("1/1/0001 12:00:00 AM");

        private DateTime _startDateTime;

        public DateTime HeaderDate
        {
            get { return _startDateTime.ToLocalTime(); }
            set { _startDateTime = value.ToUniversalTime(); }
        }
    }
}