using KLib.Concurrent.Inter;
using KLib.Concurrent.Locker;

namespace KLib.Concurrent.Queue
{
    public class SingleQueue<T> : IQueueIn<T>, IQueueOut<T>
    {
        #region Properties
        private const string ERROR_INVALID_OPERATION_EXCEPTION = "Invalid operation",
            ERROR_OPERATION_CANCELED_EXCEPTION = "CancellationToken was canceled";
        private static readonly T[] ARRAY_EMPTY = [];

        private readonly Queue<T> queue;
        private readonly TicketLock lockObject;
        private InterIntStruct interCount;
        private InterBoolStruct isAvailable;
        public int Count => interCount.Value;

        public bool IsAvailable => isAvailable.Value;
        #endregion

        #region Construction
        public SingleQueue()
        {
            queue = [];
            lockObject = new();
            interCount = new();
            isAvailable = new();
        }
        public SingleQueue(int capacity)
        {
            queue = new(Math.Max(0, capacity));
            lockObject = new();
            interCount = new();
            isAvailable = new();
        }
        #endregion

        #region Method
        public void Active()
        {
            isAvailable.Exchange(true);
        }
        #endregion

        #region In Add
        public void Disable()
        {
            isAvailable.Exchange(false);
        }
        public void Enqueue(T value)
        {
            if (!IsAvailable)
                return;
            //
            Ticket ticket = lockObject.Wait();
            //
            try
            {
                if (!IsAvailable)
                    return;
                //
                queue.Enqueue(value);
                interCount.Increment();
            }
            finally
            {
                ticket.Release();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task EnqueueAsync(T value, CancellationToken cancellationToken = default)
        {
            if (!IsAvailable)
                throw new InvalidOperationException(ERROR_INVALID_OPERATION_EXCEPTION);
            //
            TicketAccept ticket = await lockObject.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                throw new OperationCanceledException(ERROR_OPERATION_CANCELED_EXCEPTION);
            //
            try
            {
                if (!IsAvailable)
                    throw new InvalidOperationException(ERROR_INVALID_OPERATION_EXCEPTION);
                //
                queue.Enqueue(value);
                interCount.Increment();
            }
            finally
            {
                ticket.Release();
            }
        }
        public bool TryEnqueue(T value)
        {
            if (!IsAvailable)
                return false;
            //
            Ticket ticket = lockObject.Wait();
            //
            try
            {
                if (!IsAvailable)
                    return false;
                //
                queue.Enqueue(value);
                interCount.Increment();
                //
                return true;
            }
            finally
            {
                ticket.Release();
            }
        }
        public async Task<bool> TryEnqueueAsync(T value, CancellationToken cancellationToken = default)
        {
            if (!IsAvailable)
                return false;
            //
            TicketAccept ticket = await lockObject.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                return false;
            //
            try
            {
                if (!IsAvailable)
                    return false;
                //
                queue.Enqueue(value);
                interCount.Increment();
                //
                return true;
            }
            finally
            {
                ticket.Release();
            }
        }
        #endregion

        #region Dequeue
        public T Dequeue()
        {
            Ticket ticket = lockObject.Wait();
            //
            try
            {
                T value = queue.Dequeue();
                interCount.Decrement();
                //
                return value;
            }
            finally
            {
                ticket.Release();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<T> DequeueAsync(CancellationToken cancellationToken = default)
        {
            TicketAccept ticket = await lockObject.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                throw new OperationCanceledException(ERROR_OPERATION_CANCELED_EXCEPTION);
            //
            try
            {
                T value = queue.Dequeue();
                interCount.Decrement();
                //
                return value;
            }
            finally
            {
                ticket.Release();
            }
        }
        public bool TryDequeue(out T? value)
        {
            Ticket ticket = lockObject.Wait();
            //
            try
            {
                if (queue.TryDequeue(out value))
                {
                    interCount.Decrement();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
            finally
            {
                ticket.Release();
            }
        }
        #endregion

        #region Dequeue All
        public T[] DequeueAll()
        {
            Ticket ticket = lockObject.Wait();
            //
            T[] values;
            if (queue.Count == 0)
            {
                values = ARRAY_EMPTY;
            }
            else
            {
                values = [.. queue];
                queue.Clear();
                interCount.Exchange(0);
            }
            //
            ticket.Release();
            return values;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<T[]> DequeueAllAsync(CancellationToken cancellationToken = default)
        {
            TicketAccept ticket = await lockObject.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                throw new OperationCanceledException(ERROR_OPERATION_CANCELED_EXCEPTION);
            //
            T[] values;
            if (queue.Count == 0)
            {
                values = ARRAY_EMPTY;
            }
            else
            {
                values = [.. queue];
                queue.Clear();
                interCount.Exchange(0);
            }
            //
            ticket.Release();
            return values;
        }
        public bool TryDequeueAll(out T[] values)
        {
            Ticket ticket = lockObject.Wait();
            //
            try
            {
                if (queue.Count == 0)
                {
                    values = ARRAY_EMPTY;
                    return false;
                }
                //
                values = [.. queue];
                queue.Clear();
                interCount.Exchange(0);
                //
                return true;
            }
            finally
            {
                ticket.Release();
            }
        }
        #endregion

        #region Out Clear
        public T[] Clear()
        {
            Ticket ticket = lockObject.Wait();
            //
            T[] values;
            if (queue.Count == 0)
            {
                values = ARRAY_EMPTY;
            }
            else
            {
                values = [.. queue];
                queue.Clear();
                interCount.Exchange(0);
            }
            //
            ticket.Release();
            return values;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<T[]> ClearAsync(CancellationToken cancellationToken = default)
        {
            TicketAccept ticket = await lockObject.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                throw new OperationCanceledException(ERROR_OPERATION_CANCELED_EXCEPTION);
            //
            T[] values;
            if (queue.Count == 0)
            {
                values = ARRAY_EMPTY;
            }
            else
            {
                values = [.. queue];
                queue.Clear();
                interCount.Exchange(0);
            }
            //
            ticket.Release();
            return values;
        }
        public bool TryClear([NotNullWhen(true)] out T[]? values)
        {
            Ticket ticket = lockObject.Wait();
            //
            try
            {
                if (queue.Count == 0)
                {
                    values = ARRAY_EMPTY;
                    return false;
                }
                //
                values = [.. queue];
                queue.Clear();
                interCount.Exchange(0);
                //
                return true;
            }
            finally
            {
                ticket.Release();
            }
        }
        #endregion
    }
}
