namespace KemLibCore.Concurrent.Inter
{
    public struct InterByteStruct
    {
        #region Properties
        private const byte default_value = 0;
        private byte value;

        public byte Value
        {
            get => Interlocked.CompareExchange(ref value, default_value, default_value);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterByteStruct()
        {
            value = 0;
        }
        public InterByteStruct(byte value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public short Exchange(byte value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(byte value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(byte value, out byte currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public byte CompareExchange(byte value, byte comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(byte value, byte comparand)
        {
            byte currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(byte value, byte comparand, out byte currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        #endregion
    }
}
