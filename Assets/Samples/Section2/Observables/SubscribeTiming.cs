using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class SubscribeTiming : MonoBehaviour
    {
        /// <summary>
        /// 剩餘時間
        /// </summary>
        [SerializeField] private float _countTimeSeconds = 30f;

        /// <summary>
        /// Observable 通知你超時
        /// </summary>
        public IObservable<Unit> OnTimeUpAsyncSubject => _onTimeUpAsyncSubject;

        /// <summary>
        /// AsyncSubject（只能發出一次消息的Subject）
        /// </summary>
        private readonly AsyncSubject<Unit> _onTimeUpAsyncSubject
            = new AsyncSubject<Unit>();
        
        private IDisposable _disposable;

        private void Start()
        {
            // 當指定的時間過去時通知消息
            _disposable = Observable
                .Timer(TimeSpan.FromSeconds(_countTimeSeconds))
                .Subscribe(_ =>
                {
                    //當定時器觸發時發出單元類型(Unit)消息
                    _onTimeUpAsyncSubject.OnNext(Unit.Default);
                    _onTimeUpAsyncSubject.OnCompleted();
                });
        }

        private void OnDestroy()
        {
            // 如果 Observable 仍在運行，則停止
            _disposable?.Dispose();

            //Dispose AsyncSubject
            _onTimeUpAsyncSubject.Dispose();
        }
    }
}