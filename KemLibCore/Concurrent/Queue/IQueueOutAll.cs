namespace KLib.Concurrent.Queue
{
    public interface IQueueOutAll<T>
    {
        #region Properties

        #endregion

        #region Dequeue All
        T[] DequeueAll();
        Task<T[]> DequeueAllAsync(CancellationToken cancellationToken = default);
        bool TryDequeueAll(out T[] value);
        #endregion

        #region Out Clear
        T[] Clear();
        Task<T[]> ClearAsync(CancellationToken cancellationToken = default);
        bool TryClear([NotNullWhen(true)] out T[]? values);
        #endregion
    }
}
