namespace KemLibCore.Concurrent.Inter
{
    public class InterBool
    {
        #region Properties
        private byte value;

        public bool Value
        {
            get => Interlocked.CompareExchange(ref value, 0, 0) == 1;
            set
            {
                if (value)
                    Interlocked.Exchange(ref this.value, 1);
                else
                    Interlocked.Exchange(ref this.value, 0);
            }
        }
        #endregion

        #region Construction
        public InterBool()
        {
            value = 0;
        }
        public InterBool(bool value)
        {
            if (value)
                this.value = 1;
            else
                this.value = 0;
        }
        #endregion

        #region Method
        public bool Exchange(bool value)
        {
            if (value)
                return Interlocked.Exchange(ref this.value, 1) == 1;
            else
                return Interlocked.Exchange(ref this.value, 0) == 1;
        }
        public bool TryExchange(bool value)
        {
            if (value)
                return Interlocked.Exchange(ref this.value, 1) != 1;
            else
                return Interlocked.Exchange(ref this.value, 0) != 0;
        }
        public bool TryExchange(bool value, out bool currentValue)
        {
            if (value)
                currentValue = Interlocked.Exchange(ref this.value, 1) == 1;
            else
                currentValue = Interlocked.Exchange(ref this.value, 0) == 1;
            return value != currentValue;
        }
        public int CompareExchange(bool value, bool comparand)
        {
            if (value)
            {
                if (comparand)
                    return Interlocked.CompareExchange(ref this.value, 1, 1);
                else
                    return Interlocked.CompareExchange(ref this.value, 1, 0);
            }
            else
            {
                if (comparand)
                    return Interlocked.CompareExchange(ref this.value, 0, 1);
                else
                    return Interlocked.CompareExchange(ref this.value, 0, 0);
            }
        }
        public bool TryCompareExchange(bool value, bool comparand)
        {
            bool currentValue;
            if (value)
            {
                if (comparand)
                    currentValue = Interlocked.CompareExchange(ref this.value, 1, 1) == 1;
                else
                    currentValue = Interlocked.CompareExchange(ref this.value, 1, 0) == 1;
            }
            else
            {
                if (comparand)
                    currentValue = Interlocked.CompareExchange(ref this.value, 0, 1) == 1;
                else
                    currentValue = Interlocked.CompareExchange(ref this.value, 0, 0) == 1;
            }
            return currentValue == comparand && currentValue != value;
        }
        public bool TryCompareExchange(bool value, bool comparand, out bool currentValue)
        {
            if (value)
            {
                if (comparand)
                    currentValue = Interlocked.CompareExchange(ref this.value, 1, 1) == 1;
                else
                    currentValue = Interlocked.CompareExchange(ref this.value, 1, 0) == 1;
            }
            else
            {
                if (comparand)
                    currentValue = Interlocked.CompareExchange(ref this.value, 0, 1) == 1;
                else
                    currentValue = Interlocked.CompareExchange(ref this.value, 0, 0) == 1;
            }
            return currentValue == comparand && currentValue != value;
        }
        #endregion
    }
}
