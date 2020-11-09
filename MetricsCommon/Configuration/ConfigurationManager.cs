using MetricsCommon.Serialization;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

namespace MetricsCommon.Configuration
{
    public class ConfigurationManager: IDisposable
    {
        private const string SharedConfigName = "monitoringtestappconfig";
        private const string SharedConfigMutexName = "monitoringtestappconfigmutex";
        private Mutex _syncMutex = null;
        private MemoryMappedFile _configSharedFile = null;

        public void Publish(Configuration config)
        {
            var data = StringSerializator.SerializeToString(config);
            var size = Encoding.UTF8.GetByteCount(data);
            _configSharedFile = MemoryMappedFile.CreateNew(SharedConfigName, size + 1);

            _syncMutex = new Mutex(false, SharedConfigMutexName);
            try
            {
                _syncMutex.WaitOne();

                using (var stream = _configSharedFile.CreateViewStream())
                {
                    var writer = new StreamWriter(stream);
                    writer.Write(data);
                    writer.Flush();
                }
            }
            finally
            {
                _syncMutex.ReleaseMutex();
            }
        }

        public static Configuration GetConfig()
        {
            Mutex mutex;
            if(Mutex.TryOpenExisting(SharedConfigMutexName, out mutex))
            {
                using (var mf = MemoryMappedFile.OpenExisting(SharedConfigName))
                using (var stream = mf.CreateViewStream())
                {
                    try
                    {
                        mutex.WaitOne();
                        var reader = new StreamReader(stream);
                        var data = reader.ReadToEnd();
                        return StringSerializator.Deserialize<Configuration>(data);
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
            return null;
        }

        public void Dispose()
        {
            if(_syncMutex != null)
            {
                try
                {
                    _syncMutex.WaitOne();
                    if (_configSharedFile != null)
                    {
                        _configSharedFile.Dispose();
                        _configSharedFile = null;
                    }
                }
                finally
                {
                    _syncMutex.ReleaseMutex();
                }

                _syncMutex.Dispose();
                _syncMutex = null;
            }
        }
    }
}
