using System;
using System.Collections.Generic;

namespace AbcBank
{
    using System.Collections;
    using System.Threading;

    public class ConcurrentList<T> : IList<T>, IDisposable
    {
        #region Fields
        private readonly List<T> _list;
        private readonly ReaderWriterLockSlim _lock;
        #endregion

        #region Constructors
        public ConcurrentList()
        {
            this._lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            this._list = new List<T>();
        }

        public ConcurrentList(int capacity_)
        {
            this._lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            this._list = new List<T>(capacity_);
        }

        public ConcurrentList(IEnumerable<T> items_)
        {
            this._lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            this._list = new List<T>(items_);
        }
        #endregion

        #region Methods
        public void Add(T item_)
        {
            try
            {
                this._lock.EnterWriteLock();
                this._list.Add(item_);
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }

        public void Insert(int index_, T item_)
        {
            try
            {
                this._lock.EnterWriteLock();
                this._list.Insert(index_, item_);
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }

        public bool Remove(T item_)
        {
            try
            {
                this._lock.EnterWriteLock();
                return this._list.Remove(item_);
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }

        public void RemoveAt(int index_)
        {
            try
            {
                this._lock.EnterWriteLock();
                this._list.RemoveAt(index_);
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }

        public int IndexOf(T item_)
        {
            try
            {
                this._lock.EnterReadLock();
                return this._list.IndexOf(item_);
            }
            finally
            {
                this._lock.ExitReadLock();
            }
        }

        public void Clear()
        {
            try
            {
                this._lock.EnterWriteLock();
                this._list.Clear();
            }
            finally
            {
                this._lock.ExitWriteLock();
            }
        }

        public bool Contains(T item_)
        {
            try
            {
                this._lock.EnterReadLock();
                return this._list.Contains(item_);
            }
            finally
            {
                this._lock.ExitReadLock();
            }
        }

        public void CopyTo(T[] array_, int arrayIndex_)
        {
            try
            {
                this._lock.EnterReadLock();
                this._list.CopyTo(array_, arrayIndex_);
            }
            finally
            {
                this._lock.ExitReadLock();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ConcurrentEnumerator<T>(this._list, this._lock);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ConcurrentEnumerator<T>(this._list, this._lock);
        }

        ~ConcurrentList()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing_)
        {
            if (disposing_)
                GC.SuppressFinalize(this);

            this._lock.Dispose();
        }
        #endregion

        #region Properties
        public T this[int index_]
        {
            get
            {
                try
                {
                    this._lock.EnterReadLock();
                    return this._list[index_];
                }
                finally
                {
                    this._lock.ExitReadLock();
                }
            }
            set
            {
                try
                {
                    this._lock.EnterWriteLock();
                    this._list[index_] = value;
                }
                finally
                {
                    this._lock.ExitWriteLock();
                }
            }
        }

        public int Count
        {
            get
            {
                try
                {
                    this._lock.EnterReadLock();
                    return this._list.Count;
                }
                finally
                {
                    this._lock.ExitReadLock();
                }
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion
    }

    public class ConcurrentEnumerator<T> : IEnumerator<T>
    {
        #region Fields
        private readonly IEnumerator<T> _inner;
        private readonly ReaderWriterLockSlim _lock;
        #endregion

        #region Constructor
        public ConcurrentEnumerator(IEnumerable<T> inner_, ReaderWriterLockSlim lock_)
        {
            this._lock = lock_;
            this._lock.EnterReadLock();
            this._inner = inner_.GetEnumerator();
        }
        #endregion

        #region Methods
        public bool MoveNext()
        {
            return _inner.MoveNext();
        }

        public void Reset()
        {
            _inner.Reset();
        }

        public void Dispose()
        {
            this._lock.ExitReadLock();
        }
        #endregion

        #region Properties
        public T Current
        {
            get { return _inner.Current; }
        }

        object IEnumerator.Current
        {
            get { return _inner.Current; }
        }
        #endregion
    }
}
