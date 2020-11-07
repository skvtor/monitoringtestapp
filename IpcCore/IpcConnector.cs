using IpcCore.WcfPipesChannel;
using MetricsCommon.Configuration;

namespace IpcCore
{
    public static class IpcConnector
    {
        public static IIpcClient Connect(Configuration config)
        {
            var client = new WcfPipeClient(config.ConfigItems["hubPipeUri"]);
            client.Connect();
            return client;
        }

        public static IIpcServer StartServer(Configuration config)
        {
            var server = new WcfPipeServer(config.ConfigItems["hubPipeUri"]);
            server.Start();
            return server;
        }
    }
}
