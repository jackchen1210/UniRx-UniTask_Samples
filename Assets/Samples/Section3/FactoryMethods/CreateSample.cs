using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class CreateSample : MonoBehaviour
    {
        private void Start()
        {
            //每隔一秒生成以“A”開頭的字母
            var observable = Observable.Create<char>(observer =>
            {
                // 帶有 IDisposable 和 CancellationToken 的對象
                // 當Dispose()完成後，會處於取消狀態
                var disposable = new CancellationDisposable();

                // 在執行緒池上運行
                Task.Run(async () =>
                {
                    // 將字母排列 'A' - 'Z'
                    for (var i = 0; i < 26; i++)
                    {
                        // 等待一秒
                        await Task.Delay(TimeSpan.FromSeconds(1), disposable.Token);

                        // 發布字元消息
                        observer.OnNext((char) ('A' + i));
                    }

                    //發出 OnCompleted 消息，因為一切都已完成
                    observer.OnCompleted();
                }, disposable.Token);
                // 如果 Subscribe() 被中斷，它會一起工作
                // CancellationToken 也會被取消
                return disposable;
            });

            //生成並訂閱 Observable
            observable.Subscribe(
                x => Debug.Log("OnNext:" + x),
                ex => Debug.LogError("OnError:" + ex.Message),
                () => Debug.Log("OnCompleted")
            ).AddTo(this);
        }
    }
}