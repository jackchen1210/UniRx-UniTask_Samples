using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample2 : MonoBehaviour
    {
        private void Start()
        {
            // 使用 CancellationToken 
            Observable
                .FromCoroutine(token => WaitingCoroutine(token))
                .Subscribe(
                    _ => Debug.Log("OnNext"),
                    () => Debug.Log("OnCompleted"))
                .AddTo(this);
        }

        // 接受CancellationToken
        private IEnumerator WaitingCoroutine(CancellationToken token)
        {
            Debug.Log("Coroutine start.");

            //當作為協程等待 Observable 時，
            //我希望 Observable 在這個協程停止時等待並返回停止。
            //因此使用 CancellationToken。
            yield return Observable
                .Timer(TimeSpan.FromSeconds(3))
                .ToYieldInstruction(token);

            Debug.Log("Coroutine finish.");
        }
    }
}