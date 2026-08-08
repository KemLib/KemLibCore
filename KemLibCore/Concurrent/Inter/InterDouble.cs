namespace KLib.Concurrent.Inter
{
    public class InterDouble
    {
        #region Properties
        private double value;

        public double Value
        {
            get => Interlocked.CompareExchange(ref value, 0, 0);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterDouble()
        {
            value = 0;
        }
        public InterDouble(double value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public double Exchange(double value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(double value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(double value, out double currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public double CompareExchange(double value, double comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(double value, double comparand)
        {
            double currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(double value, double comparand, out double currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        #endregion
    }
}
