namespace KLib.LogData
{
    public interface ILogHander
    {
        #region Properties
        static readonly ILogHander LogHanderNone = new LogHanderNone();

        bool IsAvaiable
        {
            get;
        }
        #endregion

        #region Method
        ILogHander CreateHander();
        Task<ILogHander> CreateHanderAsync();
        void DisableLog();
        void LogMessage(string? source = null, string? title = null, string? message = null);
        Task LogMessageAsync(string? source = null, string? title = null, string? message = null);
        void LogWarning(string? source = null, string? title = null, string? message = null);
        Task LogWarningAsync(string? source = null, string? title = null, string? message = null);
        void LogError(string? source = null, string? title = null, string? message = null);
        Task LogErrorAsync(string? source = null, string? title = null, string? message = null);
        #endregion
    }
}
