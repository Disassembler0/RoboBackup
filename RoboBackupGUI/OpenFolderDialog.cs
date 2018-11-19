using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RoboBackup {
    /// <summary>
    /// Windows Vista style folder selection dialog. Allows for selection of Libraries or UNC paths. Also allows copypasting the directory path.
    /// </summary>
    /// The class heavily relies on P/Invoke, COM imports and black magic. Courtesy of Simon Mourier - https://stackoverflow.com/a/15386992
    public class OpenFolderDialog : Component {
        /// <summary>
        /// Selected directory full path.
        /// </summary>
        public string SelectedPath { get; set; }

        /// <summary>
        /// Runs a common dialog box with a default owner.
        /// </summary>
        /// <returns><see cref="DialogResult.OK"/> if the user clicks OK in the dialog box; otherwise, <see cref="DialogResult.Cancel"/>.</returns>
        public DialogResult ShowDialog() {
            return ShowDialog(IntPtr.Zero);
        }

        /// <summary>
        /// Runs a common dialog box with the specified owner.
        /// </summary>
        /// <param name="hwndOwner">Any object that implements IWin32Window that represents the top-level window that will own the modal dialog box.</param>
        /// <returns><see cref="DialogResult.OK"/> if the user clicks OK in the dialog box; otherwise, <see cref="DialogResult.Cancel"/>.</returns>
        public DialogResult ShowDialog(IntPtr hwndOwner) {
            IFileOpenDialog dialog = (IFileOpenDialog)new FileOpenDialog();
            try {
                IShellItem item;
                if (!string.IsNullOrEmpty(SelectedPath)) {
                    uint atts = 0;
                    if (SHILCreateFromPath(SelectedPath, out IntPtr idl, ref atts) == 0) {
                        if (SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item) == 0) {
                            dialog.SetFolder(item);
                        }
                        Marshal.FreeCoTaskMem(idl);
                    }
                }
                dialog.SetOptions(0x20 | 0x40); // FOS_PICKFOLDERS | FOS_FORCEFILESYSTEM
                uint hresult = dialog.Show(hwndOwner);
                if (hresult == 0x800704C7) // ERROR_CANCELLED
                    return DialogResult.Cancel;
                if (hresult != 0)
                    return DialogResult.Abort;
                dialog.GetResult(out item);
                item.GetDisplayName(0x80058000, out string path); // SIGDN_FILESYSPATH
                SelectedPath = path;
                return DialogResult.OK;
            } finally {
                Marshal.ReleaseComObject(dialog);
            }
        }

        /// <summary>
        /// Creates a pointer to an item identifier list (PIDL) from a path.
        /// </summary>
        /// <param name="pszPath">A pointer to a null-terminated string of maximum length MAX_PATH containing the path to be converted.</param>
        /// <param name="ppIdl">The path in <paramref name="pszPath"/> expressed as a PIDL.</param>
        /// <param name="rgflnOut">A pointer to a DWORD value that, on entry, indicates any attributes of the folder named in <paramref name="pszPath"/> that the calling application would like to retrieve along with the PIDL. On exit, this value contains those requested attributes.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shilcreatefrompath
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHILCreateFromPath(string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

        /// <summary>
        /// Creates and initializes a Shell item object from a pointer to a parent item identifier list (PIDL). The resulting shell item object supports the <see cref="IShellItem"/> interface.
        /// </summary>
        /// <param name="pidlParent">A PIDL to the parent. This value can be NULL.</param>
        /// <param name="psfParent">A pointer to the parent <see cref="IShellFolder"/>. This value can be NULL.</param>
        /// <param name="pidl">A PIDL to the requested item. If parent information is not included in pidlParent or psfParent, this must be an absolute PIDL.</param>
        /// <param name="ppsi">When this method returns, contains the interface pointer to the new <see cref="IShellItem"/>.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shcreateshellitem
        [DllImport("shell32.dll")]
        private static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);

        /// <summary>
        /// Basic Vista-style file open dialog. Allows for selection of Libraries or UNC paths. Also allows copypasting the directory path.
        /// </summary>
        [ComImport, Guid("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7")]
        private class FileOpenDialog { }

        /// <summary>
        /// Extends the IFileDialog interface by adding methods specific to the open dialog.
        /// </summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/shobjidl_core/nn-shobjidl_core-ifileopendialog
        [ComImport, Guid("42F85136-DB7E-439C-85F1-E4075D135FC8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IFileOpenDialog {
            [PreserveSig]
            uint Show([In] IntPtr parent); // IModalWindow
            void SetFileTypes(); // not fully defined
            void SetFileTypeIndex([In] uint iFileType);
            void GetFileTypeIndex(out uint piFileType);
            void Advise(); // not fully defined
            void Unadvise();
            void SetOptions([In] int fos);
            void GetOptions(out int pfos);
            void SetDefaultFolder(IShellItem psi);
            void SetFolder(IShellItem psi);
            void GetFolder(out IShellItem ppsi);
            void GetCurrentSelection(out IShellItem ppsi);
            void SetFileName([In, MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);
            void SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
            void SetOkButtonLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszText);
            void SetFileNameLabel([In, MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
            void GetResult(out IShellItem ppsi);
            void AddPlace(IShellItem psi, int alignment);
            void SetDefaultExtension([In, MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);
            void Close(int hr);
            void SetClientGuid(); // not fully defined
            void ClearClientData();
            void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);
            void GetResults([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenum); // not fully defined
            void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppsai); // not fully defined
        }

        /// <summary>
        /// Exposes methods that retrieve information about a Shell item.
        /// </summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/shobjidl_core/nn-shobjidl_core-ishellitem
        [ComImport, Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItem {
            void BindToHandler(); // not fully defined
            void GetParent(); // not fully defined
            void GetDisplayName([In] uint sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
            void GetAttributes();  // not fully defined
            void Compare();  // not fully defined
        }
    }
}
