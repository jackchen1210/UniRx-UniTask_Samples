using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample3 : MonoBehaviour
    {
        /// <summary>
        /// 長按判斷閾值
        /// </summary>
        private readonly float _longPressThresholdSeconds = 1.0f;

        private void Start()
        {
            // Observable 檢測一段時間內的長按
            Observable.FromCoroutine<bool>(observer => LongPushCoroutine(observer))
                .DistinctUntilChanged() //刪除連續重複的消息
                .Subscribe(x => Debug.Log(x)).AddTo(this);
        }

        /// <summary>
        /// 檢測長按空格鍵的協程
        /// 如果按住一段時間返回 True
        /// 釋放鍵時返回False
        /// </summary>
        private IEnumerator LongPushCoroutine(IObserver<bool> observer)
        {
            var isPushed = false;
            var lastPushTime = Time.time;

            while (true)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (!isPushed)
                    {
                        //按下後立即記住時間
                        lastPushTime = Time.time;
                        isPushed = true;
                    }
                    else if (Time.time - lastPushTime > _longPressThresholdSeconds)
                    {
                        //如果按下一段時間，則發出 True
                        observer.OnNext(true);
                    }
                }
                else
                {
                    if (isPushed)
                    {
                        //發佈時發出 False
                        observer.OnNext(false);
                        isPushed = false;
                    }
                }

                yield return null;
            }
        }
    }
}