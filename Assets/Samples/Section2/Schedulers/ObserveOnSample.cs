using System.IO;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Schedulers
{
    public class ObserveOnSample : MonoBehaviour
    {
        private void Start()
        {
            // 在執行緒池上讀取文件的過程
            var task = Task.Run(() => File.ReadAllText(@"data.txt"));

            // Task 變換為 Observable
            // 此時的執行上下文仍然是線程池
            task.ToObservable()
                // 將執行上下文切換到主執行緒
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(x =>
                {
                    // 當你到達這裡時，執行上下文切換到主線程
                    Debug.Log(x);
                });
        }
    }
}