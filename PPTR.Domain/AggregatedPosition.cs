namespace PPTR.Domain
{
    public class AggregatedPosition
    {
        public string? Period { get; set; }
        public double Volume { get; set; }

        public AggregatedPosition() { }
        public AggregatedPosition(string period, double volume)
        {
            Period = period;
            Volume = volume;
        }
    }
}
