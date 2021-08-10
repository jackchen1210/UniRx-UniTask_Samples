using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.MyObservers
{
    class ObserveEventComponent2 : MonoBehaviour
    {
        [SerializeField] private CountDownEventProvider _countDownEventProvider;

        private IDisposable _disposable;

        private void Start()
        {
            // 調用Subject的Subscribe註冊觀察者
            _disposable = _countDownEventProvider
                .CountDownObservable
                .Subscribe(
                    x => Debug.Log(x), //OnNext
                    ex => Debug.LogError(ex), //OnError
                    () => Debug.Log("OnCompleted!")); //OnCompleted
        }

        private void OnDestroy()
        {
            // 銷毀GameObject時暫停事件訂閱
            _disposable?.Dispose();
        }
    }
}