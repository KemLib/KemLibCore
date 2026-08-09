namespace KLib.Concurrent.Locker
{
    public struct Int64Lock
    {
        #region Properties
        private const int DEFAULT_MILLISECONDS_DELAY = 1,
            DEFAULT_SLEEP1_THRESHOLD = 10;

        private long taget,
            current;

        public bool IsLock => Interlocked.CompareExchange(ref taget, 0, 0) >= Interlocked.CompareExchange(ref current, 0, 0);
        #endregion

        #region Construction
        public Int64Lock()
        {
            taget = 0;
            current = taget + 1;
        }
        #endregion

        #region Method
        public void Enter(int sleep1Threshold = DEFAULT_SLEEP1_THRESHOLD)
        {
            long newTaget = Interlocked.Increment(ref taget);
            if (newTaget == Interlocked.CompareExchange(ref current, 0, 0))
                return;
            //
            sleep1Threshold = Math.Max(1, sleep1Threshold);
            SpinWait spinWait = new();
            do
            {
                spinWait.SpinOnce(sleep1Threshold);
            } while (newTaget != Interlocked.CompareExchange(ref current, 0, 0));
        }
        public async Task EnterAsync(int millisecondsDelay = DEFAULT_MILLISECONDS_DELAY)
        {
            long newTaget = Interlocked.Increment(ref taget);
            if (newTaget == Interlocked.CompareExchange(ref current, 0, 0))
                return;
            //
            millisecondsDelay = Math.Max(DEFAULT_MILLISECONDS_DELAY, millisecondsDelay);
            do
            {
                await Task.Delay(millisecondsDelay);
            } while (newTaget != Interlocked.CompareExchange(ref current, 0, 0));
        }
        public void Exit()
        {
            Interlocked.Increment(ref current);
        }
        #endregion
    }
}
