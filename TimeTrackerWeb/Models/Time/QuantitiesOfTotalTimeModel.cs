namespace TimeTrackerWeb.Models.Time
{
    public interface IQuantitiesOfTotalTimeModel
    {
        public double TotalSeconds { get; set; }
        public double TotalMinutes { get; set; }
        public double TotalHours { get; set; }
        public double TotalDays { get; set; }
        public double TotalWeeks { get; set; }
        public double TotalMonths { get; set; }
        public double TotalYears { get; set; }
        public double TotalDecades { get; set; }
        public double TotalCenturies { get; set; }
        public double TotalMillenniums { get; set; }
    }
    
    public class QuantitiesOfTotalTimeModel : IQuantitiesOfTotalTimeModel
    {
        public double TotalSeconds { get; set; }
        public double TotalMinutes { get; set; }
        public double TotalHours { get; set; }
        public double TotalDays { get; set; }
        public double TotalWeeks { get; set; }
        public double TotalMonths { get; set; }
        public double TotalYears { get; set; }
        public double TotalDecades { get; set; }
        public double TotalCenturies { get; set; }
        public double TotalMillenniums { get; set; }
    }
}