using KemLibCore.Concurrent.Queue;

namespace KemLibCore.DebugLog
{
    public class LogQueue : IQueueOutAll<LogData>
    {
        #region Properties
        private readonly TwiceQueue<LogData> twiceQueue;

        public int Count => twiceQueue.Count;
        #endregion

        #region Constructions
        public LogQueue()
        {
            twiceQueue = new();
        }
        #endregion

        #region CreateHander
        public ILogHander CreateHander()
        {
            IQueueIn<LogData> queueIn = twiceQueue.CreateQueueIn();
            return new LogQueueHander(this, queueIn);
        }
        public async Task<ILogHander> CreateHanderAsync()
        {
            IQueueIn<LogData> queueIn = await twiceQueue.CreateQueueInAsync();
            return new LogQueueHander(this, queueIn);
        }
        #endregion

        #region Dequeue All
        public LogData[] DequeueAll()
        {
            return twiceQueue.DequeueAll();
        }
        public async Task<LogData[]> DequeueAllAsync(CancellationToken cancellationToken = default)
        {
            return await twiceQueue.DequeueAllAsync(cancellationToken);
        }
        public bool TryDequeueAll(out LogData[] values)
        {
            return twiceQueue.TryDequeueAll(out values);
        }
        #endregion

        #region Out Clear
        public LogData[] Clear()
        {
            return twiceQueue.Clear();
        }
        public async Task<LogData[]> ClearAsync(CancellationToken cancellationToken = default)
        {
            return await twiceQueue.DequeueAllAsync(cancellationToken);
        }
        public bool TryClear([NotNullWhen(true)] out LogData[]? values)
        {
            return twiceQueue.TryClear(out values);
        }
        #endregion
    }
}
