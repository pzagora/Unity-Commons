using System;
using System.Collections.Generic;

namespace Commons
{
    /// <summary>
    /// Adapter class for the <c>IUpdatable</c> interface. It implements the update event and exposes an API for derived classes to use.
    /// Additionally, the <c>BaseUpdatable</c> class allows you to postpone update events to support atomic changes.
    /// <para>
    /// Internally, it uses a lock counter — the number of <c>Lock()</c> and <c>Unlock()</c> calls must match.
    /// This feature allows developers to create nested methods that update the object without triggering intermediate updates.
    /// </para>
    /// </summary>
    ///
    /// <example>
    /// <code>
    /// public class Door : BaseObservable
    /// {
    ///     private Color color;
    ///     public Color Color
    ///     {
    ///         get => color;
    ///         set
    ///         {
    ///             color = value;
    ///             SignalUpdate();
    ///         }
    ///     }
    /// 
    ///     public bool IsDirty { get; private set; }
    ///
    ///     public void Clean()
    ///     {
    ///         IsDirty = false;
    ///         SignalUpdate();
    ///     }
    /// 
    ///     // This method changes two properties. To ensure atomicity,
    ///     // the update event is locked at the start and unlocked at the end.
    ///     public void CleanAndPaint(Color newColor)
    ///     {
    ///         Lock();
    ///         Clean();
    ///         Color = newColor;
    ///         Unlock();
    ///     }
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class BaseObservable : IObservable
    {
        public event Action Update;

        private int _lockCount;
        private bool _updateAfterUnlock;
        
        private int _batchDepth;
        private bool _batchDirty;
     
        /// <summary>
        /// Marks object as changed. If the object is not locked, <c>update</c> event is fired immediately. Otherwise,
        /// the <c>update</c> event will be fired when object is unlocked
        /// </summary>
        protected void SignalUpdate()
        {
            if (_batchDepth > 0)
            {
                _batchDirty = true;
                return;
            }

            if (_lockCount == 0)
            {
                Update?.Invoke();
            }
            else
            {
                _updateAfterUnlock = true;
            }
        }

        protected IDisposable Batch()
        {
            _batchDepth++;
            return new BatchToken(() =>
            {
                if (--_batchDepth == 0 && _batchDirty)
                {
                    _batchDirty = false;
                    SignalUpdate();
                }
            });
        }

        /// <summary>
        /// Increases locks counter. 
        /// </summary>
        public void Lock() => _lockCount++;

        /// <summary>
        /// Decreases locks counter. If the locks counter reaches 0 and object is change (<c>SignalUpdate</c> method was
        /// invoked), the <c>update</c> event is fired
        /// </summary>
        public void Unlock()
        {
            _lockCount = Math.Max(0, _lockCount - 1);
            
            if (_lockCount == 0 && _updateAfterUnlock)
            {
                SignalUpdate();
            }
        }
        
        /// <summary>
        /// Changes value of class field and notifies of update
        /// </summary>
        /// <param name="value">Value to change</param>
        /// <param name="newValue">New value</param>
        /// <typeparam name="T">Type of value</typeparam>
        /// <example>
        /// <code>
        /// public class Door : BaseObservable
        /// {
        ///      private Color color;
        ///      public Color Color
        ///      {
        ///          get => color;
        ///          set => UpdateValue(ref color, value);
        ///      }
        /// }
        /// </code>
        /// </example>
        protected void UpdateValue<T>(ref T value, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(value, newValue)) 
                return;
            
            value = newValue;
            SignalUpdate();
        }

        private sealed class BatchToken : IDisposable
        {
            private Action _end;

            public BatchToken(Action end)
            {
                _end = end;
            }

            public void Dispose()
            {
                _end?.Invoke();
                _end = null;
            }
        }
    }
}