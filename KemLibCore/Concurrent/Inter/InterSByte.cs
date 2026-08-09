namespace KemLibCore.Concurrent.Inter
{
    public class InterSByte
    {
        #region Properties
        private const sbyte default_value = 0;
        private sbyte value;

        public sbyte Value
        {
            get => Interlocked.CompareExchange(ref value, default_value, default_value);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterSByte()
        {
            value = 0;
        }
        public InterSByte(sbyte value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public short Exchange(sbyte value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(sbyte value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(sbyte value, out sbyte currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public sbyte CompareExchange(sbyte value, sbyte comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(sbyte value, sbyte comparand)
        {
            sbyte currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(sbyte value, sbyte comparand, out sbyte currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        #endregion
    }
}
