namespace KLib.Concurrent.Inter
{
    public class InterObject<T> where T : class
    {
        #region Properties
        private T? value;

        public T? Value
        {
            get => Interlocked.CompareExchange(ref value, null, null);
            set => Interlocked.Exchange(ref this.value, value);
        }
        #endregion

        #region Construction
        public InterObject()
        {
            value = null;
        }
        public InterObject(T? value)
        {
            this.value = value;
        }
        #endregion

        #region Method
        public T? Exchange(T? value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }
        public bool TryExchange(T? value)
        {
            T? oldValue = Interlocked.Exchange(ref this.value, value);
            if (oldValue is null)
                return value is not null;
            return !oldValue.Equals(value);
        }
        public bool TryExchange(T? value, out T? currentValue)
        {
            currentValue = Interlocked.Exchange(ref this.value, value);
            if (currentValue is null)
                return value is not null;
            return !currentValue.Equals(value);
        }
        public T? CompareExchange(T? value, T? comparand)
        {
            return Interlocked.CompareExchange(ref this.value, value, comparand);
        }
        public bool TryCompareExchange(T? value, T? comparand)
        {
            T? currentValue = Interlocked.CompareExchange(ref this.value, value, comparand);
            if (currentValue is null)
                return comparand is null && value is not null;
            else
                return currentValue.Equals(comparand) && !currentValue.Equals(value);
        }
        public bool TryCompareExchange(T? value, T? comparand, out T? currentValue)
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
