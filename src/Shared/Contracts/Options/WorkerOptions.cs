namespace Shared.Contracts.Options
{
    public class WorkerOptions
    {
        public const string SectionName = "Worker";
        public int IntervalInSeconds { get; set; }
        public string Ispb { get; set; }
    }
}