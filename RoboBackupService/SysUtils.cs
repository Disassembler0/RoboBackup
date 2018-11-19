using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace RoboBackup {
    /// <summary>
    /// Filesystem entry type.
    /// </summary>
    public enum EntryType {
        File,
        Directory
    }

    /// <summary>
    /// Filesystem modification and robocopy process utilities.
    /// </summary>
    public static class SysUtils {
        /// <summary>
        /// Deletes logs older than <see cref="Config.LogRetention"/> days.
        /// </summary>
        public static void DeleteOldLogs() {
            DateTime threshold = DateTime.Now.AddDays(-Config.LogRetention);
            DirectoryInfo logRoot = new DirectoryInfo(Config.LogRoot);
            foreach (DirectoryInfo directory in logRoot.EnumerateDirectories()) {
                foreach (FileInfo file in directory.EnumerateFiles().Where(f => f.LastWriteTime < threshold)) {
                    try {
                        file.Delete();
                        Logger.LogLogFileDeleted(file.FullName);
                    } catch { /* Ignore if the old log can't be deleted now */ }
                }
            }
        }

        /// <summary>
        /// Prepares and executes robocopy backup command.
        /// </summary>
        /// <param name="task"><see cref="Task"/> to run.</param>
        public static void RunBackup(Task task) {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string logFile = GetLogName(task.Guid, timestamp);
            string destDir = Path.Combine(task.Destination, timestamp);
            string latestLink = Path.Combine(task.Destination, "latest");
            try {
                if (task.Method == Method.Differential && task.Retention > 1) {
                    try {
                        CopyDirectoryStructure(latestLink, destDir);
                        CopyHardlinkStructure(latestLink, destDir);
                    } catch { /* Ignore if the latestLink doesn't exist or points to invalid directory */ }
                }
                string uncPath = Unc.IsUncPath(task.Source) ? task.Source : Unc.IsUncPath(task.Destination) ? task.Destination : null;
                using (Unc unc = new Unc(uncPath)) {
                    unc.Connect(Credential.Decrypt(task.Guid, task.Credential));
                    RunRobocopy(task, logFile, destDir);
                }
                if (task.Retention > 1) {
                    CreateLatestSymlink(destDir, latestLink);
                    DeleteOldDirs(task.Destination, task.Retention);
                }
            } catch (Exception e) {
                using (StreamWriter streamWriter = new StreamWriter(logFile, true)) {
                    streamWriter.Write(e.Message);
                    streamWriter.Write(e.StackTrace);
                }
            }
        }

        /// <summary>
        /// Copies directory structure from source to destination.
        /// </summary>
        /// <param name="sourceDir">Source directory.</param>
        /// <param name="destDir">Destination directory.</param>
        private static void CopyDirectoryStructure(string sourceDir, string destDir) {
            string args = string.Format("{0} {1} /E /CREATE /DST /DCOPY:DAT /XF * /XJ /R:0 /W:0", EscapeArg(sourceDir), EscapeArg(destDir));
            ProcessStartInfo psi = new ProcessStartInfo("robocopy", args) { WindowStyle = ProcessWindowStyle.Hidden };
            Process robocopy = Process.Start(psi);
            robocopy.WaitForExit();
        }

        /// <summary>
        /// Copies file structure from source to destination as hardlinks.
        /// </summary>
        /// <param name="sourceDir">Source directory path.</param>
        /// <param name="destDir">Destination directory path.</param>
        private static void CopyHardlinkStructure(string sourceDir, string destDir) {
            foreach (string dir in Directory.EnumerateDirectories(sourceDir)) {
                string dest = Path.Combine(destDir, Path.GetFileName(dir));
                CopyHardlinkStructure(dir, dest);
            }
            foreach (string file in Directory.EnumerateFiles(sourceDir)) {
                string dest = Path.Combine(destDir, Path.GetFileName(file));
                CreateHardlink(dest, file);
            }
        }

        /// <summary>
        /// Creates a hardlink.
        /// </summary>
        /// <param name="lpFileName">New hardlink file path.</param>
        /// <param name="lpExistingFileName">Existing file path to point the hardlink to.</param>
        private static void CreateHardlink(string lpFileName, string lpExistingFileName) {
            CreateHardlink(lpFileName, lpExistingFileName, IntPtr.Zero);
        }

        /// <summary>
        /// Creates a hardlink.
        /// </summary>
        /// <param name="lpFileName">New hardlink file path.</param>
        /// <param name="lpExistingFileName">Existing file path to point the hardlink to.</param>
        /// <param name="lpSecurityAttributes">Security attributes - unused.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CreateHardLinkW", CharSet = CharSet.Unicode)]
        private static extern int CreateHardlink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        /// <summary>
        /// Creates a symlink to the latest backup directory.
        /// </summary>
        /// <param name="destDir">Path of the latest backup directory.</param>
        /// <param name="latestLink">Symlink path.</param>
        private static void CreateLatestSymlink(string destDir, string latestLink) {
            if (Directory.Exists(latestLink)) {
                Directory.Delete(latestLink);
            }
            CreateSymlink(latestLink, destDir, (int)EntryType.Directory);
        }

        /// <summary>
        /// Creates a symbolic link.
        /// </summary>
        /// <param name="lpSymlinkFileName">New symlink file path.</param>
        /// <param name="lpTargetFileName">Existing file or directory path to point the symlink to.</param>
        /// <param name="dwFlags"><see cref="EntryType"/> of the existing file or directory.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CreateSymbolicLinkW", CharSet = CharSet.Unicode)]
        private static extern int CreateSymlink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        /// <summary>
        /// Recursively deletes a directory.
        /// </summary>
        /// <param name="path">Path of the directory to delete.</param>
        /// Directory.Delete() is slow as it enumerates the content of the directory. Furthermore it doesn't allow to delete files with read-only attribute without unsetting it first.
        private static void DeleteDir(string path) {
            ProcessStartInfo psi = new ProcessStartInfo("cmd", string.Format("/C rmdir /s /q {0}", EscapeArg(path))) { WindowStyle = ProcessWindowStyle.Hidden };
            Process rmdir = Process.Start(psi);
            rmdir.WaitForExit();
            Logger.LogDirectoryDeleted(path);
        }

        /// <summary>
        /// Deletes old backups. Leaves the latest <see cref="Task.Retention"/> directories and deletes the rest.
        /// </summary>
        /// <param name="dir"><see cref="Task"/> log directory.</param>
        /// <param name="retention">Number of the latest backups to retain.</param>
        private static void DeleteOldDirs(string dir, ushort retention) {
            string[] subdirs = Directory.GetDirectories(dir);
            if (subdirs.Length > ++retention) {
                foreach (string subdir in subdirs.Take(subdirs.Length - retention)) {
                    DeleteDir(subdir);
                }
            }
        }

        /// <summary>
        /// Escapes command line argument.
        /// </summary>
        /// <param name="arg">Argument to escape.</param>
        /// <returns></returns>
        private static string EscapeArg(string arg) {
            return "\"" + Regex.Replace(arg, @"(\\+)$", @"$1$1") + "\"";
        }

        /// <summary>
        /// Generates a log file name for the current run of the <see cref="Task"/>.
        /// </summary>
        /// <param name="guid">GUID of the <see cref="Task"/>.</param>
        /// <param name="timestamp">Formatted <see cref="DateTime"/> of the <see cref="RunBackup"/> call.</param>
        /// <returns></returns>
        private static string GetLogName(string guid, string timestamp) {
            string logDir = Path.Combine(Config.LogRoot, guid);
            if (!Directory.Exists(logDir)) {
                Directory.CreateDirectory(logDir);
            }
            return Path.Combine(logDir, string.Format("{0}.log", timestamp));
        }

        /// <summary>
        /// Compiles the robocopy backup <see cref="ProcessStartInfo"/> arguments and executes the robocopy <see cref="Process"/>.
        /// </summary>
        /// <param name="task"><see cref="Task"/> to run.</param>
        /// <param name="logFile">Robocopy log file path.</param>
        /// <param name="destDir">Backup destination directory path.</param>
        private static void RunRobocopy(Task task, string logFile, string destDir) {
            List<string> args = new List<string>() { EscapeArg(task.Source) };
            if (task.Retention == 1) {
                args.Add(EscapeArg(task.Destination));
                args.Add(task.Method == Method.Incremental ? "/E" : "/MIR");
            } else {
                args.Add(EscapeArg(destDir));
                args.Add("/MIR");
            }
            if (task.ExcludedDirs.Length > 0) {
                args.Add("/XD");
                args.AddRange(task.ExcludedDirs.Select(x => EscapeArg(Path.Combine(task.Source, x))));
            }
            if (task.ExcludedFiles.Length > 0) {
                args.Add("/XF");
                args.AddRange(task.ExcludedFiles.Select(x => EscapeArg(Path.Combine(task.Source, x))));
            }
            args.Add(string.Format("/XJ /SL /DST /ZB /DCOPY:DAT /COPY:DAT /R:2 /W:3 /NP \"/UNILOG+:{0}\"", logFile));
            ProcessStartInfo psi = new ProcessStartInfo("robocopy", string.Join(" ", args)) { WindowStyle = ProcessWindowStyle.Hidden };
            Process robocopy = Process.Start(psi);
            robocopy.WaitForExit();
        }

        /*
         * Usage :: ROBOCOPY source destination [file [file]...] [options]
         * /E :: copy subdirectories, including Empty ones.
         * /PURGE :: delete dest files/dirs that no longer exist in source.
         * /MIR :: MIRror a directory tree (equivalent to /E plus /PURGE).
         * /CREATE :: CREATE directory tree and zero-length files only.
         * /XF file [file]... :: eXclude Files matching given names/paths/wildcards.
         * /XD dirs [dirs]... :: eXclude Directories matching given names/paths.
         * /XJ :: eXclude Junction points and symbolic links. (normally included by default).
         * /SL :: copy symbolic links versus the target.
         * /DST :: compensate for one-hour DST time differences.
         * /ZB :: use restartable mode; if access denied use Backup mode.
         * /DCOPY:copyflag[s] :: what to COPY for directories (default is /DCOPY:DA).
         *             (copyflags : D=Data, A=Attributes, T=Timestamps).
         * /COPY:copyflag[s] :: what to COPY for files (default is /COPY:DAT).
         *             (copyflags : D=Data, A=Attributes, T=Timestamps).
         *             (S=Security=NTFS ACLs, O=Owner info, U=aUditing info).
         * /R:n :: number of Retries on failed copies: default 1 million.
         * /W:n :: Wait time between retries: default is 30 seconds.
         * /NP :: No Progress - don't display percentage copied.
         * /UNILOG+:file :: output status to LOG file as UNICODE (append to existing log).
         */
    }
}
