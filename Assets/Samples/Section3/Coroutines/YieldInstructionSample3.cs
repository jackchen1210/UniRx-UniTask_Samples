using System;
using System.Collections;
using System.IO;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class YieldInstructionSample3 : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(ReadFileCoroutine());
        }

        /// <summary>
        /// 使用協程等待異步讀取文件
        /// </summary>
        private IEnumerator ReadFileCoroutine()
        {
            // 將文件的異步讀取轉換為 YieldInstruction
            // 如果你為 throwOnError 指定了 false，
            // 失敗時將保持異常
            // (如果為真，異常將按原樣拋出）
            var yi = ReadFileAsync(@"data.txt")
                .ToYieldInstruction(throwOnError: false);

            // 等待完成
            yield return yi;

            if (yi.HasError) //你可以用 HasError 判斷成功或失敗
            {
                //OnError 異常儲存在 Error 中
                Debug.LogError(yi.Error);
            }
            else
            {
                // 成功的結果儲存在 Result
                Debug.Log(yi.Result);
            }
        }

        /// <summary>
        /// 創建一個異步讀取文件的 IObservable
        /// </summary>
        private IObservable<string> ReadFileAsync(string path)
        {
            return Observable.Start(() =>
            {
                using (var r = new StreamReader(path))
                {
                    return r.ReadToEnd();
                }
            }, Scheduler.ThreadPool);
        }
    }
}