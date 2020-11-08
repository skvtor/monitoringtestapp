using MetricsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsHub
{
    internal class TestProcessMetricAggregation: IAggregationContainer
    {
        int _maxCpuUsagePid = -1;
        double _maxCpuUsage = 0;
        int _maxMemoryUsagePid = -1;
        double _maxMemoryUsage = 0;

        ReaderWriterLock _lock = new ReaderWriterLock();

        public void Aggregate(MetricBase metric)
        {
            var procMetric = metric as ProcessDescriptorMetric;
            if (procMetric == null)
                return;

            try
            {
                _lock.AcquireWriterLock(Timeout.Infinite);

                if(procMetric.MemoryUsage > _maxMemoryUsage)
                {
                    _maxMemoryUsage = procMetric.MemoryUsage;
                    _maxMemoryUsagePid = procMetric.Pid;
                }

                if (procMetric.CpuPerc > _maxCpuUsage)
                {
                    _maxCpuUsage = procMetric.CpuPerc;
                    _maxCpuUsagePid = procMetric.Pid;
                }

                if(procMetric.Pid == _maxCpuUsagePid)
                    _maxCpuUsage = procMetric.CpuPerc;

                if (procMetric.Pid == _maxMemoryUsagePid)
                    _maxMemoryUsage = procMetric.MemoryUsage;

            }
            finally
            {
                _lock.ReleaseWriterLock();
            }
        }

        public object GetAggregation()
        {
            try
            {
                _lock.AcquireReaderLock(Timeout.Infinite);

                return new TestProcessMetrics
                {
                    MaxCpuUsagePid = _maxCpuUsagePid,
                    MaxCpuUsage = _maxCpuUsage,
                    MaxMemoryUsagePid = _maxMemoryUsagePid,
                    MaxMemoryUsage = _maxMemoryUsage
                };
            }
            finally
            {
                _lock.ReleaseReaderLock();
            }
        }
    }
}
