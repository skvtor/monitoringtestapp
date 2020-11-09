namespace MetricsRestService.Models
{
    public class ProcessesMetricsModelOut
    {
        public int MaxCpuUsagePid { get; set; }
        public double MaxCpuUsage { get; set; }
        public int MaxMemoryUsagePid { get; set; }
        public double MaxMemoryUsage { get; set; }
    }
}
