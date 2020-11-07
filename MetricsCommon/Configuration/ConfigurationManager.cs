using MetricsCommon.Serialization;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsCommon.Configuration
{
    public class ConfigurationManager: IDisposable
    {
        const string SharedConfigName = "monitoringtestappconfig";
        const string SharedConfigMutexName = "monitoringtestappconfigmutex";
        Mutex _syncMutex = null;
        MemoryMappedFile _configSharedFile = null;

        public async Task Publish(Configuration config)
        {
            var data = Serializator.SerializeToString(config);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(data);
            _configSharedFile = MemoryMappedFile.CreateNew(SharedConfigName, size+1);

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

        public static async Task<Configuration> GetConfig()
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
                        var data = await reader.ReadToEndAsync();
                        return Serializator.Deserialize<Configuration>(data);
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
