namespace KLib.DebugLog
{
    internal class LogHanderNone : ILogHander
    {
        #region Properties
        public bool IsAvaiable => false;
        #endregion

        #region Construction
        public LogHanderNone()
        {

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

        }
        public void LogError(string? source = null, string? title = null, string? message = null)
        {

        }
        public async Task LogErrorAsync(string? source = null, string? title = null, string? message = null)
        {

        }
        public void LogMessage(string? source = null, string? title = null, string? message = null)
        {

        }
        public async Task LogMessageAsync(string? source = null, string? title = null, string? message = null)
        {

        }
        public void LogWarning(string? source = null, string? title = null, string? message = null)
        {

        }
        public async Task LogWarningAsync(string? source = null, string? title = null, string? message = null)
        {

        }
        #endregion
    }
}
