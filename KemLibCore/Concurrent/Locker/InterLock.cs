namespace KLib.Concurrent.Locker
{
    public struct InterLock
    {
        #region Properties
        private const int DEFAULT_MILLISECONDS_DELAY = 1,
            DEFAULT_SLEEP1_THRESHOLD = 10;

        private byte value;
        private SpinWait spinWait;

        public bool IsLock => Interlocked.CompareExchange(ref value, 0, 0) == 1;
        #endregion

        #region Construction
        public InterLock()
        {
            value = 0;
            spinWait = new();
        }
        #endregion

        #region Method
        public void Enter(int sleep1Threshold = DEFAULT_SLEEP1_THRESHOLD)
        {
            if (Interlocked.Exchange(ref value, 1) == 0)
                return;
            //
            sleep1Threshold = Math.Max(1, sleep1Threshold);
            do
            {
                spinWait.SpinOnce(sleep1Threshold);
            } while (Interlocked.Exchange(ref value, 1) == 1);
        }
        public async Task EnterAsync(int millisecondsDelay = DEFAULT_MILLISECONDS_DELAY)
        {
            if (Interlocked.Exchange(ref value, 1) == 0)
                return;
            //
            millisecondsDelay = Math.Max(DEFAULT_MILLISECONDS_DELAY, millisecondsDelay);
            do
            {
                await Task.Delay(millisecondsDelay);
            } while (Interlocked.Exchange(ref value, 1) == 1);
        }
        public void Exit()
        {
            Interlocked.Exchange(ref value, 0);
        }
        #endregion
    }
}
