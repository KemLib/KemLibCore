using KLib.Concurrent.Inter;
using KLib.Concurrent.Locker;

namespace KLib.Concurrent.Queue
{
    internal class TwiceQueueIn<T> : IQueueIn<T>
    {
        #region Properties
        private const string ERROR_INVALID_OPERATION_EXCEPTION = "Invalid operation",
            ERROR_OPERATION_CANCELED_EXCEPTION = "CancellationToken was canceled";

        private InterInt intetIndex;
        private List<T> valueIn,
            valueOut;
        private List<int> indexIn,
            indexOut;
        private readonly TicketLock lockDicIn;
        private InterBoolStruct isAvailable;

        public bool IsAvailable => isAvailable.Value;
        #endregion

        #region Construction
        internal TwiceQueueIn(InterInt intetIndex)
        {
            this.intetIndex = intetIndex;
            valueIn = [];
            valueOut = [];
            indexIn = [];
            indexOut = [];
            lockDicIn = new();
            isAvailable = new(true);
        }
        #endregion

        #region Swap
        internal void Swap(InterInt intetIndex)
        {
            Ticket ticket = lockDicIn.Wait();
            //
            try
            {
                (valueIn, valueOut) = (valueOut, valueIn);
                (indexIn, indexOut) = (indexOut, indexIn);
                //
                this.intetIndex = intetIndex;
            }
            catch (Exception)
            {

            }
            finally
            {
                ticket.Release();
            }
        }
        internal async Task SwapAsync(InterInt intetIndex)
        {
            Ticket ticket = await lockDicIn.WaitAsync();
            //
            try
            {
                (valueIn, valueOut) = (valueOut, valueIn);
                (indexIn, indexOut) = (indexOut, indexIn);
                //
                this.intetIndex = intetIndex;
            }
            catch (Exception)
            {

            }
            finally
            {
                ticket.Release();
            }
        }
        #endregion

        #region Method
        internal void Dequeue(T[] values)
        {
            int count = indexOut.Count;
            for (int i = 0; i < count; i++)
                values[indexOut[i]] = valueOut[i];
            valueOut.Clear();
            indexOut.Clear();
        }
        #endregion

        #region In
        public void Disable()
        {
            isAvailable.Exchange(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Enqueue(T value)
        {
            if (!IsAvailable)
                throw new InvalidOperationException(ERROR_INVALID_OPERATION_EXCEPTION);
            //
            Ticket ticket = lockDicIn.Wait();
            try
            {
                if (!IsAvailable)
                    throw new InvalidOperationException(ERROR_INVALID_OPERATION_EXCEPTION);
                //
                int index = intetIndex.Increment() - 1;
                indexIn.Add(index);
                valueIn.Add(value);
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
        /// <exception cref="Exception"></exception>
        public async Task EnqueueAsync(T value, CancellationToken cancellationToken = default)
        {
            if (!IsAvailable)
                throw new InvalidOperationException(ERROR_INVALID_OPERATION_EXCEPTION);
            //
            TicketAccept ticket = await lockDicIn.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                throw new OperationCanceledException(ERROR_OPERATION_CANCELED_EXCEPTION);
            //
            try
            {
                if (!IsAvailable)
                    throw new InvalidOperationException(ERROR_INVALID_OPERATION_EXCEPTION);
                //
                int index = intetIndex.Increment() - 1;
                indexIn.Add(index);
                valueIn.Add(value);
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
            Ticket ticket = lockDicIn.Wait();
            try
            {
                if (!IsAvailable)
                    return false;
                //
                int index = intetIndex.Increment() - 1;
                indexIn.Add(index);
                valueIn.Add(value);
                //
                return true;
            }
            catch (Exception)
            {
                return false;
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
            TicketAccept ticket = await lockDicIn.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                return false;
            //
            try
            {
                if (!IsAvailable)
                    return false;
                //
                int index = intetIndex.Increment() - 1;
                indexIn.Add(index);
                valueIn.Add(value);
                //
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ticket.Release();
            }
        }
        #endregion
    }
}
