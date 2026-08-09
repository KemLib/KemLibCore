namespace KemLibCore.Concurrent.Inter
{
    public class InterShort
    {
        #region Properties
        private const short default_value = 0;
        private short value;

        public short Value
        {
            get => Interlocked.CompareExchange(ref value, default_value, default_value);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterShort()
        {
            value = 0;
        }
        public InterShort(short value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public short Exchange(short value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(short value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(short value, out short currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public short CompareExchange(short value, short comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(short value, short comparand)
        {
            short currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(short value, short comparand, out short currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        #endregion
    }
}
