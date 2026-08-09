using KLib.Concurrent.Inter;

namespace KLib.DebugLog
{
    public class LogHanderCallback : ILogHander
    {
        #region Properties
        private readonly Action<LogData>? onLog;
        private InterBoolStruct interAvaiable;

        public bool IsAvaiable => interAvaiable.Value;
        #endregion

        #region Construction
        public LogHanderCallback(Action<LogData>? onLog = null)
        {
            this.onLog = onLog;
            interAvaiable = new(true);
        }
        #endregion

        #region CreateHander
        public ILogHander CreateHander()
        {
            return this;
        }
        public async Task<ILogHander> CreateHanderAsync()
        {
            return this;
        }
        #endregion

        #region CreateHander
        public void DisableLog()
        {
            interAvaiable.Exchange(false);
        }
        public void LogMessage(string? source = null, string? title = null, string? message = null)
        {
            if (!interAvaiable.Value || onLog == null)
                return;
            //
            LogData log = new(LogType.Message, source, title, message);
            onLog(log);
        }
        public async Task LogMessageAsync(string? source = null, string? title = null, string? message = null)
        {
            if (!interAvaiable.Value || onLog == null)
                return;
            //
            LogData log = new(LogType.Message, source, title, message);
            onLog(log);
        }
        public void LogWarning(string? source = null, string? title = null, string? message = null)
        {
            if (!interAvaiable.Value || onLog == null)
                return;
            //
            LogData log = new(LogType.Warning, source, title, message);
            onLog(log);
        }
        public async Task LogWarningAsync(string? source = null, string? title = null, string? message = null)
        {
            if (!interAvaiable.Value || onLog == null)
                return;
            //
            LogData log = new(LogType.Warning, source, title, message);
            onLog(log);
        }
        public void LogError(string? source = null, string? title = null, string? message = null)
        {
            if (!interAvaiable.Value || onLog == null)
                return;
            //
            LogData log = new(LogType.Error, source, title, message);
            onLog(log);
        }
        public async Task LogErrorAsync(string? source = null, string? title = null, string? message = null)
        {
            if (!interAvaiable.Value || onLog == null)
                return;
            //
            LogData log = new(LogType.Error, source, title, message);
            onLog(log);
        }
        #endregion
    }
}
