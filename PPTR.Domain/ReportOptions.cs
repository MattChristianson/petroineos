namespace PPTR.Domain
{
    public class ReportOptions
    {
        /// <summary>
        /// Interval in Seconds
        /// </summary>
        public double Interval { get; set; }
        public string? ReportsPath { get; set; }
        public string? LogFilePath { get; set; }
        /// <summary>
        /// Service timepout in seconds
        /// </summary>
        public double Timeout { get; set; }
        /// <summary>
        /// Number of service retry attempts
        /// </summary>
        public int RetryAttempts  { get; set; }
    }
}
