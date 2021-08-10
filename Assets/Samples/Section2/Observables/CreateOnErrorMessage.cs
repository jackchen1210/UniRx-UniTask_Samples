using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class CreateOnErrorMessage : MonoBehaviour
    {
        void Start()
        {
            //建立資料流
            var subject = new Subject<string>();

            //訂閱資料流
            subject
                .Select(str => int.Parse(str)) //將字串轉換為 int，如果失敗則異常
                .Subscribe(new MyObservers.PrintLogObserver<int>());

            subject.OnNext("1");
            subject.OnNext("2");
            subject.OnNext("Three"); //int.Parse 失敗並發生異常

            subject.OnCompleted();
        }
    }
}