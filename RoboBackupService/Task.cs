using System;
using System.Threading;

namespace RoboBackup {
    /// <summary>
    /// <see cref="Task"/> method.
    /// </summary>
    public enum Method {
        Incremental,
        Differential,
        Full
    }

    /// <summary>
    /// <see cref="Task"/> period.
    /// </summary>
    public enum Period {
        Daily,
        Weekly,
        Monthly
    }

    /// <summary>
    /// Backup task definition - source and destination directories, exclusions, schedule etc.
    /// </summary>
    [Serializable]
    public class Task {
        /// <summary>
        /// Encrypted credentials for use with network shares.
        /// </summary>
        public string Credential { get; set; }

        /// <summary>
        /// Day of month for monthly schedule.
        /// </summary>
        public byte DayOfMonth { get; set; }

        /// <summary>
        /// Day of week for weekly schedule.
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// Backup destination directory.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Directories to be excluded from the backup.
        /// </summary>
        public string[] ExcludedDirs { get; set; }

        /// <summary>
        /// Files to be excluded from the backup.
        /// </summary>
        public string[] ExcludedFiles { get; set; }

        /// <summary>
        /// Global unique identifier of the <see cref="Task"/>.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Scheduled hour when to run the <see cref="Task"/>.
        /// </summary>
        public byte Hour { get; set; }

        /// <summary>
        /// Backup method.
        /// </summary>
        public Method Method { get; set; }

        /// <summary>
        /// Scheduled minute when to run the <see cref="Task"/>.
        /// </summary>
        public byte Minute { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> of the next <see cref="Task"/> run.
        /// </summary>
        public DateTime NextRunDate {
            get {
                if (_nextRunDate == DateTime.MinValue) {
                    RefreshNextRunDate();
                }
                return _nextRunDate;
            }
        }

        /// <summary>
        /// How often to run the <see cref="Task"/>.
        /// </summary>
        public Period Period { get; set; }

        /// <summary>
        /// How many backup directories to retain.
        /// </summary>
        public ushort Retention { get; set; }

        /// <summary>
        /// Backup source directory.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Non-unique human readable identifier.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Private variable backing the <see cref="NextRunDate"/> property.
        /// </summary>
        [NonSerialized]
        private DateTime _nextRunDate;

        /// <summary>
        /// <see cref="Thread"/> used to execute the robocopy backup process.
        /// </summary>
        [NonSerialized]
        private Thread _thread;

        /// <summary>
        /// Calculates <see cref="DateTime"/> of the next <see cref="Task"/> run.
        /// </summary>
        public void RefreshNextRunDate() {
            DateTime now = DateTime.Now;
            DateTime nextRun = DateTime.Today.AddHours(Hour).AddMinutes(Minute);
            if (Period == Period.Daily) {
                if (nextRun < now) {
                    nextRun = nextRun.AddDays(1);
                }
            } else if (Period == Period.Weekly) {
                nextRun = nextRun.AddDays(DayOfWeek - now.DayOfWeek);
                if (nextRun < now) {
                    nextRun = nextRun.AddDays(7);
                }
            } else {
                while (DayOfMonth > DateTime.DaysInMonth(nextRun.Year, nextRun.Month)) {
                    nextRun = nextRun.AddMonths(1);
                }
                nextRun = nextRun.AddDays(DayOfMonth - now.Day);
                if (nextRun < now) {
                    nextRun = nextRun.AddMonths(1);
                }
            }
            _nextRunDate = nextRun;
            Logger.LogTaskNextRunDate(this);
        }

        /// <summary>
        /// Starts the robocopy backup process in a separate thread.
        /// </summary>
        public void Start() {
            Logger.LogTaskStart(this);
            _thread = new Thread(new ThreadStart(() => SysUtils.RunBackup(this)));
            _thread.Start();
        }

        /// <summary>
        /// Interrupts the thread with the robocopy backup process.
        /// </summary>
        public void Stop() {
            if (_thread != null && _thread.ThreadState == ThreadState.Running) {
                _thread.Abort();
            }
        }
    }
}
