using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace RoboBackup {
    /// <summary>
    /// RoboBackup service class.
    /// </summary>
    public partial class Service : ServiceBase {
        /// <summary>
        /// Tick period in milliseconds.
        /// </summary>
        private const int _period = 60000;

        /// <summary>
        /// Threshold limit for tick skew.
        /// </summary>
        private static readonly TimeSpan _skewLimit = TimeSpan.FromSeconds(90);

        /// <summary>
        /// <see cref="DateTime"/> of the last tick.
        /// </summary>
        private static DateTime _lastTick;

        /// <summary>
        /// Main timer for schedulling <see cref="Task"/> and <see cref="LogCleaner"/> execution.
        /// </summary>
        private static Timer _timer = new Timer(OnTick, null, Timeout.Infinite, Timeout.Infinite);

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service() {
            InitializeComponent();
        }

        /// <summary>
        /// <see cref="ServiceBase.Run"/> handler. Opens <see cref="Logger"/> (if configured), loads <see cref="Config"/> and starts the <see cref="Timer"/>.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        protected override void OnStart(string[] args) {
            if (args.Length == 1 && args[0] == "/log") {
                Logger.Open(Path.Combine(Config.LogRoot, "service.log"));
            }
            Logger.LogServiceStarted();
            Config.Load();
            LogCleaner.Start();
            IpcServer.Start();
            SetNextTick(DateTime.Now);
        }

        /// <summary>
        /// <see cref="ServiceBase.Stop"/> handler. Stops the <see cref="Timer"/> and interrupts any currently running <see cref="Task"/>.
        /// </summary>
        protected override void OnStop() {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer = null;
            foreach (Task task in Config.Tasks.Values) {
                task.Stop();
            }
            Logger.LogServiceStopped();
        }

        /// <summary>
        /// Performs workload during normal tick. Runs <see cref="Task"/> or <see cref="LogCleaner"/> if they are scheduled to be run in the current tick.
        /// </summary>
        /// <param name="now"></param>
        private static void DoTick(DateTime now) {
            foreach (Task task in Config.Tasks.Values) {
                if (task.NextRunDate <= now) {
                    task.Start();
                    task.RefreshNextRunDate();
                }
            }
            if (LogCleaner.NextRunDate <= now) {
                LogCleaner.Start();
                LogCleaner.RefreshNextRunDate();
            }
        }

        /// <summary>
        /// <see cref="TimerCallback"/> to be called every tick. Checks validity of the tick, performs workload and schedues the next tick.
        /// </summary>
        /// <param name="state"></param>
        private static void OnTick(object state) {
            Logger.LogTick();
            DateTime now = DateTime.Now;
            // Recalculate next run dates if the last tick was more than 90 seconds ago or if it is in the future.
            // Such clock skew implies manual time change, huge NTP leap, machine awakened from hibernation/suspension or some other potentially unsafe state.
            if (now - _lastTick > _skewLimit || now < _lastTick) {
                Logger.LogTickSkew();
                foreach (Task task in Config.Tasks.Values) {
                    task.RefreshNextRunDate();
                }
                LogCleaner.Start();
                LogCleaner.RefreshNextRunDate();
            } else {
                DoTick(now);
            }
            // The next tick time has to be readjusted often to maintain the precision of the scheduled tasks.
            // System.Threading.Timer is ridiculously imprecise. The best case scenario skews the timer by 1 ms per minute (1 minute per ~100 days).
            // An alternative would be to create a custom timer implementaion based on high-precision multimedia timers, but
            // System.Threading.Timer is simple to use and guaranteed to be supported pretty much everywhere, so it's good enough.
            SetNextTick(now);
        }

        /// <summary>
        /// Sets the delay until the next tick.
        /// </summary>
        /// <param name="now"><see cref="DateTime"/> of the previous tick.</param>
        private static void SetNextTick(DateTime now) {
            int delay = _period - (now.Second * 1000) - now.Millisecond;
            Logger.LogTickDelay(delay);
            _timer.Change(delay, _period);
            _lastTick = now;
        }
    }
}
