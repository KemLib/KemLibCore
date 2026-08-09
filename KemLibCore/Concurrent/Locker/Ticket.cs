namespace KLib.Concurrent.Locker
{
    public readonly struct Ticket(TaskCompletionSource taskSource)
    {
        #region Properties
        private readonly TaskCompletionSource taskSource = taskSource;

        #endregion

        #region Method
        public readonly void Release()
        {
            taskSource?.TrySetResult();
        }
        #endregion
    }
}
