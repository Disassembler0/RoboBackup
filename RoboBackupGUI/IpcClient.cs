using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace RoboBackup {
    /// <summary>
    /// Client side for interprocess communication via named pipe between the GUI (client) and the service (server).
    /// </summary>
    public static class IpcClient {
        /// <summary>
        /// Interprocess communication client channel.
        /// </summary>
        private static IpcClientChannel _channel;

        /// <summary>
        /// Interprocess communication object proxy.
        /// </summary>
        private static IpcService _service;

        /// <summary>
        /// Tests connection via IPC and return <see cref="IpcService"/> object proxy.
        /// </summary>
        /// <returns><see cref="IpcService"/> object proxy.</returns>
        public static IpcService GetService() {
            try {
                _service.Ping();
            } catch {
                RegisterIpcClient();
            }
            return _service;
        }

        /// <summary>
        /// Registers interprocess communication client channel and the <see cref="IpcService"/> object proxy.
        /// </summary>
        private static void RegisterIpcClient() {
            // Unregister the channel if it was previously registered but the underlying pipe has been closed (e.g. when the service was restarted but the GUI wasn't).
            if (_channel != null) {
                ChannelServices.UnregisterChannel(_channel);
            }
            _channel = new IpcClientChannel();
            ChannelServices.RegisterChannel(_channel, false);
            if (_service == null) {
                WellKnownClientTypeEntry remoteType = new WellKnownClientTypeEntry(typeof(IpcService), "ipc://RoboBackup/ipc");
                RemotingConfiguration.RegisterWellKnownClientType(remoteType);
                _service = new IpcService();
            }
        }
    }
}
