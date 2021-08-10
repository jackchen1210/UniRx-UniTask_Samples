using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Schedulers
{
    public class CurrentThreadSample : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("Unity Main Thread ID:" + Thread.CurrentThread.ManagedThreadId);

            var subject = new Subject<Unit>();
            subject.AddTo(this);

            // 和當前執行緒處理OnNext消息一樣，就是原樣傳遞。
            subject.ObserveOn(Scheduler.Immediate)
                .Subscribe(_ =>
                {
                    Debug.Log("Thread Id:" + Thread.CurrentThread.ManagedThreadId);
                });

            // 在主執行緒中發出 OnNext
            subject.OnNext(Unit.Default);

            // 從另一個執行緒發出 OnNext
            Task.Run(() => subject.OnNext(Unit.Default));
        }
    }
}