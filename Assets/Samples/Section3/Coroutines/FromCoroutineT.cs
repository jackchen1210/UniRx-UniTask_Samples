using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineT : MonoBehaviour
    {
        private void Start()
        {
            Observable.FromCoroutineValue<Vector3>(PositionCoroutine, false)
                .Subscribe(
                    x => Debug.Log(x), 
                    () => Debug.Log("OnCompleted"));

            //Output :
            //(0,0,0)
            //(0,1,0)
            //(0,1,1)
            //(1,1,1)
            //OnCompleted
        }

        /// <summary>
        /// 按順序發出座標數組的迭代器
        /// </summary>
        private IEnumerator PositionCoroutine()
        {
            var positions = new[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 1),
                new Vector3(1, 1, 1),
            };

            foreach (var p in positions)
            {
                yield return p;
            }
        }
    }
}