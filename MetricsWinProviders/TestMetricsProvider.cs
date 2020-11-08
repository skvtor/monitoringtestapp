using MetricsCommon;
using MetricsCommon.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetricsWinProviders
{
    public class TestMetricsProvider : IMetricsProvider
    {
        public string Name => "TestMetricProvider";

        private Dictionary<int, _processinfo> _counters = new Dictionary<int, _processinfo>();
        private HashSet<int> _processedPids = new HashSet<int>();
        public List<MetricBase> CaptureMetrics()
        {
            var retVal = new List<MetricBase>();
            _processedPids.Clear();

            var processes = Process.GetProcesses();
            foreach (var proc in processes)
            {
                _processinfo pinfo;
                if (!_counters.TryGetValue(proc.Id, out pinfo))
                {
                    pinfo = new _processinfo();
                    pinfo.CpuPerc = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName);
                    pinfo.MemoryUsage = new PerformanceCounter("Process", "Working Set - Private", proc.ProcessName);
                    //pinfo.DiscUsage = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
                    //pinfo.NetworkUsage = new PerformanceCounter("Network Interface", "% Processor Time", proc.ProcessName);
                }

                var metric = new MetricBase();//new ProcessDescriptorMetric();
                try
                {
                    
                    metric.Name = proc.ProcessName;
                    metric.Pid = proc.Id;
                    metric.SessionId = proc.SessionId;
                    metric.CpuPerc = pinfo.CpuPerc.NextValue();
                    metric.MemoryUsage = pinfo.MemoryUsage.NextValue() / 1024 / 1024;
                    //metric.DiscUsage = pinfo.DiscUsage.NextValue();
                }
                catch
                {
                    //todo: ???
                    metric = null;
                }
                if(metric != null)
                    retVal.Add(metric);

                _processedPids.Add(proc.Id);
            }

            foreach(var counters in _counters)
            {
                if(!_processedPids.Contains(counters.Key))
                {
                    var info = counters.Value;
                    info.CpuPerc.Dispose();
                    info.MemoryUsage.Dispose();
                    info.DiscUsage.Dispose();
                    _counters.Remove(counters.Key);
                }
            }

            return retVal;
        }

        class _processinfo
        {
            public PerformanceCounter CpuPerc { get; set; }
            public PerformanceCounter MemoryUsage { get; set; }
            public PerformanceCounter DiscUsage { get; set; }
            public PerformanceCounter NetworkUsage { get; set; }    
        }
    }
}
