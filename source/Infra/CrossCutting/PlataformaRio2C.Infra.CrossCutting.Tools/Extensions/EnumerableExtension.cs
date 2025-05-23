﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> ie, object @lock)
        {
            return new SafeEnumerable<T>(ie, @lock);
        }
    }

    /// <summary>
    /// A thread-safe IEnumerable implementation
    /// See: http://www.codeproject.com/KB/cs/safe_enumerable.aspx
    /// </summary>
    public class SafeEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _inner;
        private readonly object _lock;

        public SafeEnumerable(IEnumerable<T> inner, object @lock)
        {
            _lock = @lock;
            _inner = inner;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SafeEnumerator<T>(_inner.GetEnumerator(), _lock);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// A thread-safe IEnumerator implementation.
    /// See: http://www.codeproject.com/KB/cs/safe_enumerable.aspx
    /// </summary>
    public class SafeEnumerator<T> : IEnumerator<T>
    {
        // this is the (thread-unsafe)
        // enumerator of the underlying collection
        private readonly IEnumerator<T> _inner;

        // this is the object we shall lock on.
        private readonly object _lock;

        public SafeEnumerator(IEnumerator<T> inner, object @lock)
        {
            _inner = inner;
            _lock = @lock;

            // entering lock in constructor
            Monitor.Enter(_lock);
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            // .. and exiting lock on Dispose()
            // This will be called when foreach loop finishes
            Monitor.Exit(_lock);
        }

        /// <remarks>
        /// we just delegate actual implementation
        /// to the inner enumerator, that actually iterates
        /// over some collection
        /// </remarks>
        public bool MoveNext()
        {
            return _inner.MoveNext();
        }

        public void Reset()
        {
            _inner.Reset();
        }
    }


    /// <summary>
    /// A thread-safe IList implementation using the custom SafeEnumerator class
    /// See: http://www.codeproject.com/KB/cs/safe_enumerable.aspx
    /// </summary>
    public class SafeList<T> : IList<T>
    {
        // the (thread-unsafe) collection that actually stores everything
        private readonly List<T> _inner;

        // this is the object we shall lock on.
        private readonly object _lock = new object();

        public SafeList()
        {
            _inner = new List<T>();
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _inner.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[int index]
        {
            get
            {
                lock (_lock)
                {
                    return _inner[index];
                }
            }
            set
            {
                lock (_lock)
                {
                    _inner[index] = value;
                }
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (_lock)
            {
                // instead of returning an usafe enumerator,
                // we wrap it into our thread-safe class
                return new SafeEnumerator<T>(_inner.GetEnumerator(), _lock);
            }
        }

        /// <remarks>
        /// To be actually thread-safe, our collection must be locked on all other operations
        /// </remarks>
        public void Add(T item)
        {
            lock (_lock)
            {
                _inner.Add(item);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _inner.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (_lock)
            {
                return _inner.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _inner.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            lock (_lock)
            {
                return _inner.Remove(item);
            }
        }

        public IEnumerator GetEnumerator()
        {
            lock (_lock)
            {
                return new SafeEnumerator<T>(_inner.GetEnumerator(), _lock);
            }
        }

        public int IndexOf(T item)
        {
            lock (_lock)
            {
                return _inner.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (_lock)
            {
                _inner.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_lock)
            {
                _inner.RemoveAt(index);
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            lock (_lock)
            {
                return new ReadOnlyCollection<T>(this);
            }
        }


        public void ForEach(Action<T> action)
        {
            lock (_lock)
            {
                foreach (var item in _inner)
                {
                    action(item);
                }
            }
        }


        public bool Exists(Predicate<T> match)
        {
            lock (_lock)
            {
                foreach (var item in _inner)
                {
                    if (match(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
