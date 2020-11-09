using MetricsHub;
using MetricsRestService.Models;
using System.Web.Http;

namespace MetricsRestService.Controllers
{
    [RoutePrefix("api/metrics")]
    public class MetricsController: ApiController
    {
        private MetricsHubService _hub;

        public MetricsController(MetricsHubService hub)
        {
            _hub = hub;
        }

        [HttpGet]
        [Route("processes")]
        public ProcessesMetricsModelOut Test()
        {
            var aggregation = _hub.GetTestMetrics();
            return new ProcessesMetricsModelOut
            {
                MaxCpuUsage = aggregation.MaxCpuUsage,
                MaxCpuUsagePid = aggregation.MaxCpuUsagePid,
                MaxMemoryUsage = aggregation.MaxMemoryUsage,
                MaxMemoryUsagePid = aggregation.MaxMemoryUsagePid,
            };
        }
    }
}
