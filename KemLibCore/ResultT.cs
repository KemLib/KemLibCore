namespace KemLibCore
{
    public readonly struct Result<T> : IResult
    {
        #region Properties
        private readonly bool isSuccess;
        private readonly string error;
        private readonly T? value;

        public readonly bool IsSuccess => isSuccess;
        public readonly string ErrorMessage => error;
        public readonly T? Value => value;
        #endregion

        #region Construction
        internal Result(bool isSuccess, string? error, T? value)
        {
            this.isSuccess = isSuccess;
            if (isSuccess)
                this.error = string.Empty;
            else
                this.error = string.IsNullOrEmpty(error) ? IResult.ERROR_MESSAGE_UNKNOWN : error;
            this.value = value;
        }
        #endregion
    }
}
