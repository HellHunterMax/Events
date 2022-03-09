using Events.Core.Shared.Extentions;

namespace Events.Core.Shared.ValueObjects
{
    public class Duration : ValueObject
    {
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }
        public int DurationInDays => (End - Start).Days;

        private Duration()
        {
        }
        public Duration(DateTime start, DateTime end)
        {
            if (start.ThrowIfNull(nameof(start)) > end.ThrowIfNull(nameof(end))) throw new ArgumentException("Start date is greated than end date");
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return $"Start {Start}, End {End}, Number of days {DurationInDays}";
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
