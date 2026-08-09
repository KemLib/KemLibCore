namespace KemLibCore.Concurrent.Inter
{
    public struct InterUIntStruct
    {
        #region Properties
        private uint value;

        public uint Value
        {
            get => Interlocked.CompareExchange(ref value, 0, 0);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterUIntStruct()
        {
            value = 0;
        }
        public InterUIntStruct(uint value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public uint Exchange(uint value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(uint value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(uint value, out uint currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public uint CompareExchange(uint value, uint comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(uint value, uint comparand)
        {
            uint currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(uint value, uint comparand, out uint currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public uint Increment()
        {
            return Interlocked.Increment(ref value);
        }
        public uint Decrement()
        {
            return Interlocked.Decrement(ref value);
        }
        public uint Add(uint value)
        {
            return Interlocked.Add(ref this.value, value);
        }
        public uint And(uint value)
        {
            return Interlocked.And(ref this.value, value);
        }
        public uint Or(uint value)
        {
            return Interlocked.Or(ref this.value, value);
        }
        #endregion
    }
}
