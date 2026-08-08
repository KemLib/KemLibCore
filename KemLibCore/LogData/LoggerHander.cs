using KLib.Concurrent.Queue;

namespace KLib.LogData
{
    public class LoggerHander(Logger logger, IQueueIn<LogData> queueIn) : ILogHander
    {
        #region Properties
        private readonly Logger logger = logger;
        private readonly IQueueIn<LogData> queueIn = queueIn;

        public bool IsAvaiable => queueIn.IsAvailable;
        #endregion

        #region Constructions

        #endregion

        #region CreateHander
        public ILogHander CreateHander()
        {
            return logger.CreateHander();
        }
        public async Task<ILogHander> CreateHanderAsync()
        {
            return await logger.CreateHanderAsync();
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
