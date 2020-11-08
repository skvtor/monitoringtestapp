using IpcCore.WcfPipesChannel;
using MetricsCommon;
using MetricsCommon.Configuration;

namespace IpcCore
{
    public static class IpcConnector
    {
        public static IIpcClient Connect(Configuration config)
        {
            var client = new WcfPipeClient(config.ConfigItems[Constants.IpcHubPipeUriParamName]);
            client.Connect();
            return client;
        }

        public static IIpcServer StartServer(Configuration config)
        {
            var server = new WcfPipeServer(config.ConfigItems[Constants.IpcHubPipeUriParamName]);
            server.Start();
            return server;
        }
    }
}
