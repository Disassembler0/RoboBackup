using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace RoboBackup {
    /// <summary>
    /// Provides methods for accessing UNC paths with shared network resources requiring credentials.
    /// </summary>
    public class Unc : IDisposable {
        /// <summary>
        /// Lifecycle state of the <see cref="Unc"/> object.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// UNC path with shared network resource to be accessed.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Prepare a new <see cref="Unc"/> instance for connection to a shared network resource.
        /// </summary>
        /// <param name="path">Network path to be accessed.</param>
        public Unc(string path) {
            _path = path;
        }

        /// <summary>
        /// Destroys <see cref="Unc"/> instance for shared network resource access.
        /// </summary>
        ~Unc() {
            Dispose();
        }

        /// <summary>
        /// Checks if <see cref="string"/> resembles an UNC path.
        /// </summary>
        /// <param name="path"><see cref="string"/> to be checked.</param>
        /// <returns><see langword="true"/> if the <paramref name="path"/> resembles UNC path, otherwise <see langword="false"/>.</returns>
        public static bool IsUncPath(string path) {
            return path.StartsWith(@"\\");
        }

        /// <summary>
        /// Resolves mapped drive into UNC network path.
        /// </summary>
        /// <param name="path">Path to be resolved.</param>
        /// <returns>Resolved UNC network path if the root of the <paramref name="path"/> is on mapped drive, unchaged <paramref name="path"/> otherwise.</returns>
        public static string TranslatePath(string path) {
            if (IsUncPath(path)) {
                return path;
            }
            string[] path_split = path.Split(new char[] { '\\' }, 2);
            StringBuilder stringBuilder = new StringBuilder(512);
            int size = stringBuilder.Capacity;
            var error = WNetGetConnectionW(path_split[0], stringBuilder, ref size);
            if (error != 0)
                return path;
            return Path.Combine(stringBuilder.ToString(), path_split[1]);
        }

        /// <summary>
        /// Accesses the UNC path with shared network resource using <see cref="Credential"/>.
        /// </summary>
        /// <param name="credential">Username and password required for access to the shared network resource.</param>
        public void Connect(Credential credential) {
            if (credential == null) {
                _disposed = true;
                return;
            }
            string[] path_split = _path.Split(new char[] { '\\' });
            string domain = path_split.Length > 2 ? path_split[2] : null;
            UseInfo2 useinfo = new UseInfo2 {
                ui2_remote = _path,
                ui2_domainname = domain,
                ui2_username = credential.Username,
                ui2_password = credential.Password,
                ui2_asg_type = 0, // USE_WILDCARD
                ui2_usecount = 1
            };
            NetUseAdd(null, 2, ref useinfo, out uint parmErr); // 2 = useinfo is USE_INFO_2
        }

        /// <summary>
        /// Disconnects from the shared network resource and sets the <see cref="Unc"/> object as disposed.
        /// </summary>
        public void Dispose() {
            if (!_disposed) {
                NetUseDel(null, _path, 2); // 2 = USE_LOTS_OF_FORCE
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The NetUseAdd function establishes a connection between the local computer and a remote server. You can specify a local drive letter or a printer device to connect. If you do not specify a local drive letter or printer device, the function authenticates the client with the server for future connections.
        /// </summary>
        /// <param name="uncServerName">The UNC name of the computer on which to execute this function. If this parameter is <see langword="null"/>, then the local computer is used.</param>
        /// <param name="levelFlags">A value that specifies the information level of the data.</param>
        /// <param name="buf">A pointer to the buffer that specifies the data. The format of this data depends on the value of the <paramref name="levelFlags"/> parameter.</param>
        /// <param name="parmErr">A pointer to a value that receives the index of the first member of the information structure in error. If this parameter is <see langword="null"/>, the index is not returned on error.</param>
        /// <returns>If the function succeeds, the return value is NERR_Success. If the function fails, the return value is a system error code.</returns>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/lmuse/nf-lmuse-netuseadd
        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern uint NetUseAdd(string uncServerName, uint levelFlags, ref UseInfo2 buf, out uint parmErr);

        /// <summary>
        /// The NetUseDel function ends a connection to a shared resource.
        /// </summary>
        /// <param name="uncServerName">The UNC name of the computer on which to execute this function. If this is parameter is NULL, then the local computer is used.</param>
        /// <param name="useName">A pointer to a string that specifies the path of the connection to delete.</param>
        /// <param name="forceLevelFlags">The level of force to use in deleting the connection.</param>
        /// <returns>If the function succeeds, the return value is NERR_Success. If the function fails, the return value is a system error code.</returns>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/lmuse/nf-lmuse-netusedel
        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern uint NetUseDel(string uncServerName, string useName, uint forceLevelFlags);

        /// <summary>
        /// Retrieves the name of the network resource associated with a local device.
        /// </summary>
        /// <param name="lpLocalName">Pointer to a constant null-terminated string that specifies the name of the local device to get the network name for.</param>
        /// <param name="lpRemoteName">Pointer to a null-terminated string that receives the remote name used to make the connection.</param>
        /// <param name="lpnLength">Pointer to a variable that specifies the size of the buffer pointed to by the lpRemoteName parameter, in characters. If the function fails because the buffer is not large enough, this parameter returns the required buffer size.</param>
        /// <returns>If the function succeeds, the return value is NO_ERROR. If the function fails, the return value is a system error code.</returns>
        [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int WNetGetConnectionW(string lpLocalName, StringBuilder lpRemoteName, ref int lpnLength);

        /// <summary>
        /// The USE_INFO_2 structure contains information about a connection between a local computer and a shared resource, including connection type, connection status, user name, and domain name.
        /// </summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/lmuse/ns-lmuse-_use_info_2
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct UseInfo2 {
            internal string ui2_local;
            internal string ui2_remote;
            internal string ui2_password;
            internal uint ui2_status;
            internal uint ui2_asg_type;
            internal uint ui2_refcount;
            internal uint ui2_usecount;
            internal string ui2_username;
            internal string ui2_domainname;
        }
    }
}
