using IpcCore.WcfPipesChannel;
using MetricsCommon;
using MetricsCommon.Configuration;

namespace IpcCore
{
    public static class IpcConnector
    {
        public static IIpcClient Connect(Configuration config)
        {
            var client = new WcfPipeClient(config.ConfigItems[Constants.Config_IpcHubPipeUri]);
            client.Connect();
            return client;
        }

        public static IIpcServer GetServer(Configuration config)
        {
            var server = new WcfPipeServer(config.ConfigItems[Constants.Config_IpcHubPipeUri]);
            return server;
        }
    }
}
