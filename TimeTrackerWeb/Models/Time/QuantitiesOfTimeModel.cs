namespace TimeTrackerWeb.Models.Time
{
    public class QuantitiesOfTimeModel : IQuantitiesOfTotalTimeModel, IQuantitiesOfRelativeTimeModel
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

        public double RelativeSeconds { get; set; }
        public bool ShouldShowRelativeSeconds { get => false; }

        public double RelativeMinutes { get; set; }
        public bool ShouldShowRelativeMinutes { get => RelativeMinutes + RelativeHours + RelativeDays + RelativeWeeks + RelativeMonths + RelativeYears + RelativeDecades + RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeHours { get; set; }
        public bool ShouldShowRelativeHours { get => RelativeHours + RelativeDays + RelativeWeeks + RelativeMonths + RelativeYears + RelativeDecades + RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeDays { get; set; }
        public bool ShouldShowRelativeDays { get => RelativeDays + RelativeWeeks + RelativeMonths + RelativeYears + RelativeDecades + RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeWeeks { get; set; }
        public bool ShouldShowRelativeWeeks { get => RelativeWeeks + RelativeMonths + RelativeYears + RelativeDecades + RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeMonths { get; set; }
        public bool ShouldShowRelativeMonths { get => RelativeMonths + RelativeYears + RelativeDecades + RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeYears { get; set; }
        public bool ShouldShowRelativeYears { get => RelativeYears + RelativeDecades + RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeDecades { get; set; }
        public bool ShouldShowRelativeDecades { get => RelativeDecades + RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeCenturies { get; set; }
        public bool ShouldShowRelativeCenturies { get => RelativeCenturies + RelativeMillenniums != 0; }

        public double RelativeMillenniums { get; set; }
        public bool ShouldShowRelativeMillenniums { get => RelativeMillenniums != 0; }
    }
}