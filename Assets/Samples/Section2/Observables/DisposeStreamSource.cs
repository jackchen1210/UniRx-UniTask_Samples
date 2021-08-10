using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class DisposeStreamSource : MonoBehaviour
    {
        //建立資料流
        private Subject<int> onChangeHpSubject = new Subject<int>();

        private IObservable<int> OnChanageHpAsObservable
        {
            get { return onChangeHpSubject; }
        }

        void Start()
        {
            //省略
        }

        /// <summary>
        /// 使用 OnDestroy 明確銷毀資料流 
        /// </summary>
        void OnDestroy()
        {
            onChangeHpSubject.Dispose();
        }
    }
}