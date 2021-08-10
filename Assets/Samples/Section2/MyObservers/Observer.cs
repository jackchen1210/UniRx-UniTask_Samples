using System;

namespace Samples.Section2.MyObservers
{
    /// <summary>
    /// 將接收到的消息輸出到日誌的觀察者
    /// </summary>
    class PrintLogObserver<T> : IObserver<T>
    {
        public void OnCompleted()
        {
            UnityEngine.Debug.Log("OnCompleted!");
        }

        public void OnError(Exception error)
        {
            UnityEngine.Debug.LogError(error);
        }

        public void OnNext(T value)
        {
            UnityEngine.Debug.Log(value);
        }
    }


    /// <summary>
    /// 簡單的觀察者介面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISimpleObserver<T>
    {
        /// <summary>
        /// 接收到事件時調用的方法
        /// </summary>
        /// <param name="message">收到事件消息</param>
        void OnReceiveEvent(T message);
    }


    /// <summary>
    /// 持有接收到的消息的觀察者，以後可以引用
    /// </summary>
    class CacheEventObserver<T> : IObserver<T>
    {
        public T ReceivedEvent { get; private set; }
        public bool IsCompleted { get; private set; }
        public Exception ReceivedError { get; private set; }

        public void OnCompleted()
        {
            IsCompleted = true;
        }

        public void OnError(Exception error)
        {
            ReceivedError = error;
        }

        public void OnNext(T value)
        {
            ReceivedEvent = value;
        }
    }

}