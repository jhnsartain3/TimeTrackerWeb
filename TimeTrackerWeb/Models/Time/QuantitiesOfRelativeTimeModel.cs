namespace TimeTrackerWeb.Models.Time
{
    public interface IQuantitiesOfRelativeTimeModel
    {
        public double RelativeSeconds { get; set; }
        public double RelativeMinutes { get; set; }
        public double RelativeHours { get; set; }
        public double RelativeDays { get; set; }
        public double RelativeWeeks { get; set; }
        public double RelativeMonths { get; set; }
        public double RelativeYears { get; set; }
        public double RelativeDecades { get; set; }
        public double RelativeCenturies { get; set; }
        public double RelativeMillenniums { get; set; }
    }
    
    public class QuantitiesOfRelativeTimeModel : IQuantitiesOfRelativeTimeModel
    {
        public double RelativeSeconds { get; set; }
        public double RelativeMinutes { get; set; }
        public double RelativeHours { get; set; }
        public double RelativeDays { get; set; }
        public double RelativeWeeks { get; set; }
        public double RelativeMonths { get; set; }
        public double RelativeYears { get; set; }
        public double RelativeDecades { get; set; }
        public double RelativeCenturies { get; set; }
        public double RelativeMillenniums { get; set; }
    }
}