using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Section3.Coroutines
{
    public class YieldInstructionSample2 : MonoBehaviour
    {
        /// <summary>
        /// uGUI的按鈕
        /// </summary>
        [SerializeField] private Button _moveButton;

        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }

        /// <summary>
        /// 按下時使對象前進 1 秒的協程
        /// </summary>
        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                // 等到按鈕被按下
                // OnClickAsObservable() 是一個無限長度的流，所以 Take(1) 將其長度限制為 1。
                yield return _moveButton.OnClickAsObservable().Take(1).ToYieldInstruction();

                var start = Time.time;
                while (Time.time - start <= 1.0f)
                {
                    transform.position += Vector3.forward * Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}