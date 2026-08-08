namespace KLib.Concurrent.Queue
{
    public interface IQueueIn<T>
    {
        #region Properties
        bool IsAvailable
        {
            get;
        }
        #endregion

        #region Method
        void Disable();
        void Enqueue(T value);
        Task EnqueueAsync(T value, CancellationToken cancellationToken = default);
        bool TryEnqueue(T value);
        Task<bool> TryEnqueueAsync(T value, CancellationToken cancellationToken = default);
        #endregion
    }
}
