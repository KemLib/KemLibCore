namespace KemLibCore.Concurrent.Locker
{
    public readonly struct TicketAccept
    {
        #region Properties
        public readonly static TicketAccept Fail = new();

        public readonly bool IsAccept;
        private readonly TaskCompletionSource? taskSource;
        #endregion

        #region Construction
        public TicketAccept()
        {
            IsAccept = false;
            taskSource = null;
        }
        public TicketAccept(TaskCompletionSource taskSource)
        {
            IsAccept = true;
            this.taskSource = taskSource;
        }
        #endregion

        #region Method
        public readonly void Release()
        {
            taskSource?.TrySetResult();
        }
        #endregion
    }
}
