using KLib.Concurrent.Inter;
using KLib.Concurrent.Locker;
using System.Diagnostics.CodeAnalysis;

namespace KLib.Concurrent.Queue
{
    public class ConcurrentList<T>
    {
        #region Properties
        private readonly List<T> list;
        private Int32Lock lockObject;
        private InterIntStruct interCount;

        public int Count => interCount.Value;
        public T this[int index] => Get(index);
        #endregion

        #region Construction
        public ConcurrentList()
        {
            list = [];
            lockObject = new();
            interCount = new();
        }
        public ConcurrentList(int capacity)
        {
            list = new(Math.Max(0, capacity));
            lockObject = new();
            interCount = new();
        }
        #endregion

        #region In Add
        public void Add(T value)
        {
            lockObject.Enter();
            try
            {
                list.Add(value);
                interCount.Increment();
            }
            finally
            {
                lockObject.Exit();
            }
        }
        public void AddRange(IEnumerable<T> values)
        {
            lockObject.Enter();
            try
            {
                list.AddRange(values);
                interCount.Exchange(list.Count);
            }
            finally
            {
                lockObject.Exit();
            }
        }
        #endregion

        #region Insert
        public void Insert(int index, T value)
        {
            lockObject.Enter();
            try
            {
                list.Insert(index, value);
                interCount.Increment();
            }
            finally
            {
                lockObject.Exit();
            }
        }
        public void InsertRange(int index, IEnumerable<T> value)
        {
            lockObject.Enter();
            try
            {
                list.InsertRange(index, value);
                interCount.Exchange(list.Count);
            }
            finally
            {
                lockObject.Exit();
            }
        }
        #endregion

        #region Remove
        public void Remove(int index)
        {
            lockObject.Enter();
            try
            {
                list.RemoveAt(index);
                interCount.Decrement();
            }
            finally
            {
                lockObject.Exit();
            }
        }
        public void RemoveAt(int index, int count)
        {
            lockObject.Enter();
            try
            {
                list.RemoveRange(index, count);
                interCount.Exchange(list.Count);
            }
            finally
            {
                lockObject.Exit();
            }
        }
        #endregion

        #region Out
        public T Get(int index)
        {
            lockObject.Enter();
            try
            {
                T value = list[index];
                return value;
            }
            finally
            {
                lockObject.Exit();
            }
        }
        public bool Get(int index, out T? value)
        {
            lockObject.Enter();
            try
            {
                value = list[index];
                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
            finally
            {
                lockObject.Exit();
            }
        }
        public T[] ToArray()
        {
            lockObject.Enter();
            try
            {
                T[] values = [.. list];
                return values;
            }
            finally
            {
                lockObject.Exit();
            }
        }
        public bool ToArray([NotNullWhen(true)] out T[]? values)
        {
            lockObject.Enter();
            try
            {
                if (list.Count == 0)
                {
                    values = default;
                    return false;
                }
                //
                values = [.. list];
                return true;
            }
            catch (Exception)
            {
                values = default;
                return false;
            }
            finally
            {
                lockObject.Exit();
            }
        }
        #endregion

        #region Clear
        public T[] Clear()
        {
            lockObject.Enter();
            try
            {
                T[] values = [.. list];
                list.Clear();
                interCount.Exchange(0);
                return values;
            }
            finally
            {
                lockObject.Exit();
            }
        }
        public bool TryClear([NotNullWhen(true)] out T[]? values)
        {
            lockObject.Enter();
            try
            {
                if (list.Count == 0)
                {
                    values = null;
                    return false;
                }
                //
                values = [.. list];
                list.Clear();
                interCount.Exchange(0);
                return true;
            }
            finally
            {
                lockObject.Exit();
            }
        }
        #endregion
    }
}
