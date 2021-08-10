using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class AddToSample : MonoBehaviour
    {
        private void Start()
        {
            // 每 5 幀發出一條消息的 Observable
            Observable.IntervalFrame(5)
                .Subscribe(_ => Debug.Log("Do!"))
                // 與此 GameObject 的 OnDestroy 一起自動 Dispose
                .AddTo(this);
        }
    }
}