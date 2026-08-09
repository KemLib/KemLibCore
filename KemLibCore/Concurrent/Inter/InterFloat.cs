namespace KemLibCore.Concurrent.Inter
{
    public class InterFloat
    {
        #region Properties
        private float value;

        public float Value
        {
            get => Interlocked.CompareExchange(ref value, 0, 0);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterFloat()
        {
            value = 0;
        }
        public InterFloat(float value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public float Exchange(float value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(float value)
        {
            return Interlocked.Exchange(ref this.value, value) != value;
        }
        public bool TryExchange(float value, out float currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            return value != currentValue;
        }
        public float CompareExchange(float value, float comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(float value, float comparand)
        {
            float currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(float value, float comparand, out float currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            return currentValue == comparand && currentValue != value;
        }
        #endregion
    }
}
