using System;
using System.Collections.Generic;
using UniRx;

namespace Samples.Section2.MySubjects
{
    /// <summary>
    /// Subject的原始實作
    /// </summary>
    public class MySubject<T> : ISubject<T>, IDisposable
    {
        public bool IsStopped { get; } = false;
        public bool IsDisposed { get; } = false;

        private readonly object _lockObject = new object();

        /// <summary>
        /// 途中發生的異常
        /// </summary>
        private Exception error;

        /// <summary>
        /// 訂閱自身的觀察者列表
        /// </summary>
        private List<IObserver<T>> observers;

        public MySubject()
        {
            observers = new List<IObserver<T>>();
        }

        /// <summary>
        /// IObserver.OnNext的實作
        /// </summary>
        public void OnNext(T value)
        {
            if (IsStopped) return;
            lock (_lockObject)
            {
                ThrowIfDisposed();

                //向所有觀察者分發消息
                foreach (var observer in observers)
                {
                    observer.OnNext(value);
                }
            }
        }

        /// <summary>
        /// IObserver.OnError的實作
        /// </summary>
        public void OnError(Exception error)
        {
            lock (_lockObject)
            {
                ThrowIfDisposed();
                if (IsStopped) return;
                this.error = error;

                try
                {
                    foreach (var observer in observers)
                    {
                        observer.OnError(error);
                    }
                }
                finally
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// IObserver.OnCompleted的實作
        /// </summary>
        public void OnCompleted()
        {
            lock (_lockObject)
            {
                ThrowIfDisposed();
                if (IsStopped) return;
                try
                {
                    foreach (var observer in observers)
                    {
                        observer.OnCompleted();
                    }
                }
                finally
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// IObservable.Subscribe的實作
        /// </summary>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            lock (_lockObject)
            {
                if (IsStopped)
                {
                    // 如果操作已經結束，則發出 OnError 或 OnCompleted
                    if (error != null)
                    {
                        observer.OnError(error);
                    }
                    else
                    {
                        observer.OnCompleted();
                    }

                    return Disposable.Empty;
                }

                //添加觀察者到列表中
                observers.Add(observer);
                return new Subscription(this, observer);
            }
        }

        private void ThrowIfDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException("MySubject");
        }

        /// <summary>
        /// 用於實現Subscribe的Dispose
        /// </summary>
        private sealed class Subscription : IDisposable
        {
            private readonly IObserver<T> _observer;
            private readonly MySubject<T> _parent;

            public Subscription(MySubject<T> parent, IObserver<T> observer)
            {
                this._parent = parent;
                this._observer = observer;
            }

            public void Dispose()
            {
                // Dispose時從觀察者列表中刪除
                _parent.observers.Remove(_observer);
            }
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                if (!IsDisposed)
                {
                    observers.Clear();
                    observers = null;
                    error = null;
                }
            }
        }
    }
}