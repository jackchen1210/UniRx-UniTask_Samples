using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Schedulers
{
    public class CompareSchedulers : MonoBehaviour
    {
        void Start()
        {
            // 它使用 Unity 的協程測量 3 秒（不阻塞主執行緒）
            Observable
                .Timer(TimeSpan.FromSeconds(3), Scheduler.MainThread)
                .Subscribe()
                .AddTo(this);

            // Thread.Sleep 主執行緒並測量 3 秒
            Observable
                .Timer(TimeSpan.FromSeconds(3), Scheduler.CurrentThread)
                .Subscribe()
                .AddTo(this);
        }
    }
}