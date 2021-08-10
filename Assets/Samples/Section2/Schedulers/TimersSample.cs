using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Schedulers
{
    public class TimersSample : MonoBehaviour
    {
        private void Start()
        {
            // 指定主線程
            // 它與經過 1 秒後立即執行的“Update()”同時執行。
            Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.MainThread)
                .Subscribe(x => Debug.Log("1秒過去了"))
                .AddTo(this);

            // 如果未指定，它將與 MainThreadScheduler 規範相同。
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(x => Debug.Log("1秒過去了"))
                .AddTo(this);

            // 當指定 MainThreadEndOfFrame 時，
            // 它將在 1 秒後立即渲染幀後執行。
            Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.MainThreadEndOfFrame)
                .Subscribe(x => Debug.Log("1秒過去了"))
                .AddTo(this);

            // 如果指定了 CurrentThread，則該程序將在與它相同的執行緒中執行。
            // 在這段程式碼的情況下，在新創建的線程上執行定時器計數
            new Thread(() =>
            {
                Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.CurrentThread)
                    .Subscribe(x => Debug.Log("1秒過去了"))
                    .AddTo(this); //執行到此時，會丟出UnityException，因為調用gameobject需使用主執行緒
            }).Start();
        }
        private void Update()
        {
            Debug.Log("Update");
            Debug.Break();
        }
    }
}