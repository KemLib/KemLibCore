namespace KLib
{
    public readonly struct Result<T1, T2, T3, T4> : IResult
    {
        #region Properties
        private readonly bool isSuccess;
        private readonly string error;
        private readonly T1? value1;
        private readonly T2? value2;
        private readonly T3? value3;
        private readonly T4? value4;

        public readonly bool IsSuccess => isSuccess;
        public readonly string ErrorMessage => error;
        public readonly T1? Value1 => value1;
        public readonly T2? Value2 => value2;
        public readonly T3? Value3 => value3;
        public readonly T4? Value4 => value4;
        #endregion

        #region Construction
        internal Result(bool isSuccess, string? error, T1? value1, T2? value2, T3? value3, T4? value4)
        {
            this.isSuccess = isSuccess;
            if (isSuccess)
                this.error = string.Empty;
            else
                this.error = string.IsNullOrEmpty(error) ? IResult.ERROR_MESSAGE_UNKNOWN : error;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
        }
        #endregion
    }
}
