using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class HotConvert : MonoBehaviour
    {
        void Start()
        {
            var originalSubject = new Subject<string>();

            //Observable 將 OnNext 的內容以空格分隔並只輸出最後一個
            //它是 IConnectableObservable <string>
            IConnectableObservable<string> appendStringObservable =
                originalSubject
                    .Scan((previous, current) => previous + " " + current)
                    .Last()
                    .Publish(); //轉變為熱資料流


            //當呼叫IConnectableObservable.Connect()時，Subscribe會在內部進行
            appendStringObservable.Connect();

            originalSubject.OnNext("I");
            originalSubject.OnNext("have");

            //可以直接訂閱 appendStringObservable
            appendStringObservable.Subscribe(x => Debug.Log(x));

            originalSubject.OnNext("a");
            originalSubject.OnNext("pen.");
            originalSubject.OnCompleted(); //I have a pen.
        }
    }
}