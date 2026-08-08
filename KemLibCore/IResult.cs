namespace KLib
{
    public interface IResult
    {
        #region Properties
        const string ERROR_MESSAGE_UNKNOWN = "Unknown";
        readonly static IResult ResultSuccess = Result.Success(),
            ResultFail = Result.Fail(string.Empty);

        bool IsSuccess
        {
            get;
        }
        string ErrorMessage
        {
            get;
        }
        #endregion

        #region Method

        #endregion
    }
}
