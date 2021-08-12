using UniRx;
using UnityEngine;

namespace Samples.Section3.FactoryMethods
{
    public class DeferSample : MonoBehaviour
    {
        private void Start()
        {
            // 每次 Subscribe() 完成時生成“返回隨機數的 Observable”
            var rand = Observable.Defer(() =>
            {
                //產生亂數
                var randomValue = UnityEngine.Random.Range(0, 100);
                // 返回隨機數的 Observable
                return Observable.Return(randomValue);
            });
            

            // 多次呼叫Subscribe()
            rand.Subscribe(x => Debug.Log(x));
            rand.Subscribe(x => Debug.Log(x));
            rand.Subscribe(x => Debug.Log(x));
        }
    }
}