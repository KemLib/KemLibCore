namespace KemLibCore
{
    public readonly struct Result : IResult
    {
        #region Properties
        public readonly static IResult ResultSuccess = Success(),
            ResultFail = Fail(string.Empty);

        private readonly bool isSuccess;
        private readonly string error;

        public readonly bool IsSuccess => isSuccess;
        public readonly string ErrorMessage => error;
        #endregion

        #region Construction
        private Result(bool isSuccess, string? error)
        {
            this.isSuccess = isSuccess;
            if (isSuccess)
                this.error = string.Empty;
            else
                this.error = string.IsNullOrEmpty(error) ? IResult.ERROR_MESSAGE_UNKNOWN : error;
        }
        #endregion

        #region Create
        public static Result Success()
        {
            return new Result(true, string.Empty);
        }
        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(true, string.Empty, value);
        }
        public static Result<T1, T2> Success<T1, T2>(T1 value1, T2 value2)
        {
            return new Result<T1, T2>(true, string.Empty, value1, value2);
        }
        public static Result<T1, T2, T3> Success<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return new Result<T1, T2, T3>(true, string.Empty, value1, value2, value3);
        }
        public static Result<T1, T2, T3, T4> Success<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            return new Result<T1, T2, T3, T4>(true, string.Empty, value1, value2, value3, value4);
        }
        public static Result Fail(string? error)
        {
            return new Result(false, error);
        }
        public static Result<T> Fail<T>(string? error)
        {
            return new Result<T>(false, error, default);
        }
        public static Result<T1, T2> Fail<T1, T2>(string? error)
        {
            return new Result<T1, T2>(false, error, default, default);
        }
        public static Result<T1, T2, T3> Fail<T1, T2, T3>(string? error)
        {
            return new Result<T1, T2, T3>(false, error, default, default, default);
        }
        public static Result<T1, T2, T3, T4> Fail<T1, T2, T3, T4>(string? error)
        {
            return new Result<T1, T2, T3, T4>(false, error, default, default, default, default);
        }
        #endregion
    }
}
