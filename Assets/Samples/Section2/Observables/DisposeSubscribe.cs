using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class DisposeSubscribe : MonoBehaviour
    {
        void Start()
        {
            var subject = new Subject<int>();

            //訂閱同一個主題 3 次（生成 3 個 Observable）
            IDisposable DisposableA = subject
                .Subscribe(x => Debug.Log("A:" + x)); //A
            IDisposable DisposableB = subject
                .Subscribe(x => Debug.Log("B:" + x)); //B
            IDisposable DisposableC = subject
                .Subscribe(x => Debug.Log("C:" + x)); //C

            //推送數值
            subject.OnNext(100);

            //Dispose A資料流
            DisposableA.Dispose();

            Debug.Log("---");

            //再次推送數值
            subject.OnNext(200);

            //結束（丟棄所有 Observable）
            subject.OnCompleted();
        }
    }
}