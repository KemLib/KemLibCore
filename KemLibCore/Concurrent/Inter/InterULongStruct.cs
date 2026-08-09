namespace KemLibCore.Concurrent.Inter
{
    public struct InterULongStruct
    {
        #region Properties
        private ulong value;

        public ulong Value
        {
            get => Interlocked.CompareExchange(ref value, 0, 0);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterULongStruct()
        {
            value = 0;
        }
        public InterULongStruct(ulong value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public ulong Exchange(ulong value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(ulong value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(ulong value, out ulong currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public ulong CompareExchange(ulong value, ulong comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(ulong value, ulong comparand)
        {
            ulong currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(ulong value, ulong comparand, out ulong currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public ulong Increment()
        {
            return Interlocked.Increment(ref value);
        }
        public ulong Decrement()
        {
            return Interlocked.Decrement(ref value);
        }
        public ulong Add(ulong value)
        {
            return Interlocked.Add(ref this.value, value);
        }
        public ulong And(ulong value)
        {
            return Interlocked.And(ref this.value, value);
        }
        public ulong Or(ulong value)
        {
            return Interlocked.Or(ref this.value, value);
        }
        #endregion
    }
}
