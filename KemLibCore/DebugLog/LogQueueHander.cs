using KLib.Concurrent.Queue;

namespace KLib.DebugLog
{
    public class LogQueueHander : ILogHander
    {
        #region Properties
        public readonly static ILogHander NONE = new LogHanderNone();

        private readonly LogQueue logQueue;
        private readonly IQueueIn<LogData> queueIn;

        public bool IsAvaiable => queueIn.IsAvailable;
        #endregion

        #region Constructions
        internal LogQueueHander(LogQueue logQueue, IQueueIn<LogData> queueIn)
        {
            this.logQueue = logQueue;
            this.queueIn = queueIn;
        }
        #endregion

        #region CreateHander
        public ILogHander CreateHander()
        {
            return logQueue.CreateHander();
        }
        public async Task<ILogHander> CreateHanderAsync()
        {
            return await logQueue.CreateHanderAsync();
        }
        #endregion

        #region Logs
        public void DisableLog()
        {
            queueIn.Disable();
        }
        public void LogMessage(string? source = null, string? title = null, string? message = null)
        {
            LogData logData = new(LogType.Message, source, title, message);
            _ = queueIn.TryEnqueue(logData);
        }
        public void LogWarning(string? source = null, string? title = null, string? message = null)
        {
            LogData logData = new(LogType.Warning, source, title, message);
            _ = queueIn.TryEnqueue(logData);
        }
        public void LogError(string? source = null, string? title = null, string? message = null)
        {
            LogData logData = new(LogType.Error, source, title, message);
            _ = queueIn.TryEnqueue(logData);
        }
        public async Task LogMessageAsync(string? source = null, string? title = null, string? message = null)
        {
            LogData logData = new(LogType.Message, source, title, message);
            _ = await queueIn.TryEnqueueAsync(logData);
        }
        public async Task LogWarningAsync(string? source = null, string? title = null, string? message = null)
        {
            LogData logData = new(LogType.Warning, source, title, message);
            _ = await queueIn.TryEnqueueAsync(logData);
        }
        public async Task LogErrorAsync(string? source = null, string? title = null, string? message = null)
        {
            LogData logData = new(LogType.Error, source, title, message);
            _ = await queueIn.TryEnqueueAsync(logData);
        }
        #endregion
    }
}
