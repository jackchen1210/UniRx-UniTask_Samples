using System;
using UnityEngine;

namespace Samples.Section2.MyObservers
{
    class ObserveEventComponent : MonoBehaviour
    {
        [SerializeField] private CountDownEventProvider _countDownEventProvider;

        // 觀察者實例
        private PrintLogObserver<int> _printLogObserver;

        private IDisposable _disposable;

        private void Start()
        {
            // 創建一個 PrintLogObserver 實例
            _printLogObserver = new PrintLogObserver<int>();

            // 調用Subject的Subscribe註冊觀察者
            _disposable = _countDownEventProvider
                .CountDownObservable.Subscribe(_printLogObserver);
        }

        private void OnDestroy()
        {
            // 銷毀GameObject時暫停事件訂閱
            _disposable?.Dispose();
        }
    }
}