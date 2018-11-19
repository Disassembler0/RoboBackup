using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace RoboBackup {
    /// <summary>
    /// Server side for interprocess communication via named pipe between the GUI (client) and the service (server).
    /// </summary>
    public static class IpcServer {
        /// <summary>
        /// Registers interprocess communication server channel and the <see cref="IpcService"/> object proxy.
        /// </summary>
        public static void Start() {
            Hashtable props = new Hashtable() {
                { "authorizedGroup", "Everyone" }, // Allow any user to connect
                { "exclusiveAddressUse", false }, // Allow to create channel if the client already has a handle on the named pipe
                { "portName", "RoboBackup" } };
            IpcServerChannel channel = new IpcServerChannel(props, null);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(IpcService), "ipc", WellKnownObjectMode.Singleton);
        }
    }
}
