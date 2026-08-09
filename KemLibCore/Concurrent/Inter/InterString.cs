namespace KLib.Concurrent.Inter
{
    public class InterString
    {
        #region Properties
        private string? value;

        public string? Value
        {
            get => Interlocked.CompareExchange(ref value, null, null);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterString()
        {
            value = null;
        }
        public InterString(string? value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public string? Exchange(string? value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(string? value)
        {
            string? oldValue = Interlocked.Exchange(ref this.value, value);
            if (oldValue is null)
                return value is not null;
            return !oldValue.Equals(value);
        }
        public bool TryExchange(string? value, out string? currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            if (currentValue is null)
                return value is not null;
            return !currentValue.Equals(value);
        }
        public string? CompareExchange(string? value, string? comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(string? value, string? comparand)
        {
            string? currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            if (currentValue is null)
                return comparand is null && value is not null;
            else
                return currentValue.Equals(comparand) && !currentValue.Equals(value);
        }
        public bool TryCompareExchange(string? value, string? comparand, out string? currentValue)
        {
            currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            if (currentValue is null)
                return comparand is null && value is not null;
            else
                return currentValue.Equals(comparand) && !currentValue.Equals(value);
        }
        #endregion
    }
}
