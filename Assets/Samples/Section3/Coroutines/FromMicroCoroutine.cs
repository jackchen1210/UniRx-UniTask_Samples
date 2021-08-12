using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromMicroCoroutineSample : MonoBehaviour
    {
        private void Start()
        {
            // 使用 FromCoroutine 轉換
            Observable
                .FromCoroutine(() => WaitingCoroutine(5))
                .Subscribe();

            //如果目標協程只使用了yield return null，
            //則可以使用更輕的FromMicroCoroutine
            Observable
                .FromMicroCoroutine(() => WaitingCoroutine(5))
                .Subscribe();
        }


        //等待指定秒數的協程
        private IEnumerator WaitingCoroutine(float seconds)
        {
            //不敢用WaitForSeconds
            var start = Time.time;
            while (Time.time - start <= seconds)
            {
                yield return null;
            }
        }
    }
}