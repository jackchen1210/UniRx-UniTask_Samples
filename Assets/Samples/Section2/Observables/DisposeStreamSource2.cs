using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class DisposeStreamSource2 : MonoBehaviour
    {
        //建立資料流
        private Subject<int> onChangeHpSubject = new Subject<int>();

        private IObservable<int> OnChanageHpAsObservable
        {
            get { return onChangeHpSubject; }
        }

        void Start()
        {
            onChangeHpSubject.AddTo(this); // Destroy後執行Subject.Dispose()
        }
    }
}