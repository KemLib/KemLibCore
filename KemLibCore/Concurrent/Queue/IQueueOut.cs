namespace KLib.Concurrent.Queue
{
    public interface IQueueOut<T> : IQueueOutAll<T>
    {
        #region Properties

        #endregion

        #region Dequeue
        T Dequeue();
        Task<T> DequeueAsync(CancellationToken cancellationToken = default);
        bool TryDequeue(out T? value);
        #endregion
    }
}
