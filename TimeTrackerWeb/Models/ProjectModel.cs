﻿using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using TimeTrackerWeb.Models.Base;
using TimeTrackerWeb.Models.Time;

namespace TimeTrackerWeb.Models
{
    public class ProjectModel : EntityBase
    {
        [Required]
        [BsonElement("name")]
        [MinLength(3)]
        public string Name { get; set; }

        [BsonElement("description")]
        [MinLength(3)]
        public string Description { get; set; }

        public QuantitiesOfTimeModel QuantitiesOfTimeModel { get; set; }
    }
}