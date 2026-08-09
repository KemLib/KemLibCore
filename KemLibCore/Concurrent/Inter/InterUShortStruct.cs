namespace KemLibCore.Concurrent.Inter
{
    public struct InterUShortStruct
    {
        #region Properties
        private const ushort default_value = 0;
        private ushort value;

        public ushort Value
        {
            get => Interlocked.CompareExchange(ref value, default_value, default_value);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterUShortStruct()
        {
            value = 0;
        }
        public InterUShortStruct(ushort value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public ushort Exchange(ushort value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(ushort value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(ushort value, out ushort currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public ushort CompareExchange(ushort value, ushort comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(ushort value, ushort comparand)
        {
            ushort currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(ushort value, ushort comparand, out ushort currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        #endregion
    }
}
