using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Samples.Section2.MyObservers
{
    /// <summary>
    /// 計算指定的秒數並通知事件
    /// </summary>
    public class CountDownEventProvider : MonoBehaviour
    {
        /// <summary>
        /// カウントする秒数
        /// </summary>
        [SerializeField] private int _countSeconds = 10;

        /// <summary>
        /// 要計數的秒數
        /// </summary>
        private Subject<int> _subject;

        /// <summary>
        /// 只暴露Subject的IObservable接口部分
        /// </summary>
        public IObservable<int> CountDownObservable => _subject;

        private void Awake()
        {
            // Subject生成
            _subject = new Subject<int>();

            // 啟動協程倒數計時
            StartCoroutine(CountCoroutine());
        }

        /// <summary>
        /// 計算指定秒數並每次發出一條消息的協程
        /// </summary>
        private IEnumerator CountCoroutine()
        {
            var current = _countSeconds;

            while (current > 0)
            {
                // 發出當前值
                _subject.OnNext(current);
                current--;
                yield return new WaitForSeconds(1.0f);
            }

            // 最後發出 0 和 OnCompleted 消息
            _subject.OnNext(0);
            _subject.OnCompleted();
        }

        private void OnDestroy()
        {
            // 銷毀GameObject時釋放Subject
            _subject.Dispose();
        }
    }
}