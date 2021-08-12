using System;
using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class CreateWithStateSample : MonoBehaviour
    {
        private void Start()
        {
            CreateCountObservable(10).Subscribe(x => Debug.Log(x));

            //Output :
            //10
            //10
            //10
            //10
            //10
            //10
            //10
            //10
            //10
            //10
        }

        /// <summary>
        /// 發出指定數量的連續值（與Range行為相同）創建並返回Observable
        /// </summary>
        /// <param name="count">指定數量</param>
        /// <returns></returns>
        IObservable<int> CreateCountObservable(int count)
        {
            // 如果計數為 0 或更少，則僅返回 OnCompleted 消息。
            if (count <= 0) return Observable.Empty<int>();

            // 發出指定數量的連續值
            return Observable.CreateWithState<int, int>(
                state: count,
                subscribe: (maxCount, observer) =>
                {
                    //state 指定的值（這裡是count）作為第一個參數傳遞，
                    //即maxCount = count
                    for (int i = 0; i < maxCount; i++)
                    {
                        observer.OnNext(maxCount);
                    }

                    observer.OnCompleted();
                    return Disposable.Empty;
                });
        }
    }
}