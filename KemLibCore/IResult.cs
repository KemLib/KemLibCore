namespace KLib
{
    public interface IResult
    {
        #region Properties
        public const string ERROR_MESSAGE_UNKNOWN = "Unknown";

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
