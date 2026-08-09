namespace KemLibCore.Concurrent.Inter
{
    public class InterLong
    {
        #region Properties
        private long value;

        public long Value
        {
            get => Interlocked.CompareExchange(ref value, 0, 0);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterLong()
        {
            value = 0;
        }
        public InterLong(long value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public long Exchange(long value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(long value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(long value, out long currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public long CompareExchange(long value, long comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(long value, long comparand)
        {
            long currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(long value, long comparand, out long currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public long Increment()
        {
            return Interlocked.Increment(ref value);
        }
        public long Decrement()
        {
            return Interlocked.Decrement(ref value);
        }
        public long Add(long value)
        {
            return Interlocked.Add(ref this.value, value);
        }
        public long And(long value)
        {
            return Interlocked.And(ref this.value, value);
        }
        public long Or(long value)
        {
            return Interlocked.Or(ref this.value, value);
        }
        #endregion
    }
}
