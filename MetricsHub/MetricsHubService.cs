using IpcCore;
using MetricsCommon.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsHub
{
    public class MetricsHubService
    {
        ConfigurationManager _configManager;
        Configuration _config;
        IIpcServer _ipcServer;
        public async Task Start(Configuration config)
        {
            _config = config;
            _configManager = new ConfigurationManager();
            await _configManager.Publish(config);

            _ipcServer = IpcConnector.StartServer(config);
        }
    }
}
