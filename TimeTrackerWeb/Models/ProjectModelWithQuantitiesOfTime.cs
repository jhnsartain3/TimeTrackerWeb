using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackerWeb.Models.Time;

namespace TimeTrackerWeb.Models
{
    public class ProjectModelWithQuantitiesOfTime : ProjectModel
    {
        public ProjectModelWithQuantitiesOfTime()
        {

        }

        public ProjectModelWithQuantitiesOfTime(ProjectModel projectModel)
        {
            Name = projectModel.Name;
            Description = projectModel.Description;
            Id = projectModel.Id;
            UserId = projectModel.UserId;
        }

        public QuantitiesOfTimeModel QuantitiesOfTimeModel { get; set; }
    }
}