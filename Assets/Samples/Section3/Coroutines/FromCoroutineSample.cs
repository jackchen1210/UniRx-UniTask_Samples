using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample : MonoBehaviour
    {
        private void Start()
        {
            // Observable 等待協程結束
            Observable.FromCoroutine(WaitingCoroutine, publishEveryYield: false)
                .Subscribe(
                    _ => Debug.Log("OnNext"), 
                    () => Debug.Log("OnCompleted"))
                .AddTo(this);

            // 還有一個縮寫叫做ToObservable()
            //WaitingCoroutine().ToObservable()
            //    .Subscribe();
        }

        // 只等待 3 秒的協程
        private IEnumerator WaitingCoroutine()
        {
            Debug.Log("Coroutine start.");

            //等待 3 秒
            yield return new WaitForSeconds(3);

            Debug.Log("Coroutine finish.");
        }
    }
}