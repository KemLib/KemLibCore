using KemLibCore.Concurrent.Inter;

namespace KemLibCore.Concurrent.Locker
{
    public class TicketLock
    {
        #region Properties
        private InterObjectStruct<Task> currentTask;
        public bool IsLock
        {
            get
            {
                Task? task = currentTask.Value;
                return task != null && !task.IsCompleted;
            }
        }
        #endregion

        #region Construction
        public TicketLock()
        {
            currentTask = new InterObjectStruct<Task>();
        }
        #endregion

        #region Wait
        public Ticket Wait()
        {
            TaskCompletionSource taskSource = new();
            Task? task = currentTask.Exchange(taskSource.Task);
            if (task == null)
                return new Ticket(taskSource);
            //
            task.Wait();
            return new Ticket(taskSource);
        }
        public async Task<Ticket> WaitAsync()
        {
            TaskCompletionSource taskSource = new();
            Task? task = currentTask.Exchange(taskSource.Task);
            if (task == null)
                return new Ticket(taskSource);
            //
            await task;
            return new Ticket(taskSource);
        }
        public async Task<TicketAccept> WaitAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return TicketAccept.Fail;
            //
            TaskCompletionSource taskSource = new();
            Task? task = currentTask.Exchange(taskSource.Task);
            if (task == null)
                return new TicketAccept(taskSource);
            //
            try
            {
                await task.WaitAsync(cancellationToken);
                return new TicketAccept(taskSource);
            }
            catch (Exception)
            {
                _ = task.ContinueWith((currentTask) =>
                {
                    taskSource.SetResult();
                }, CancellationToken.None);
                return TicketAccept.Fail;
            }
        }
        #endregion
    }
}
