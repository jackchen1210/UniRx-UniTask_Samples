using System;
using UniRx;
using UnityEngine;

namespace Samples.Section2.Observables
{
    public class PlayerHealth : MonoBehaviour
    {
        /// <summary>
        /// AsyncSubject 通知玩家已經死亡
        /// </summary>
        public IObservable<Unit> OnPlayerDeadAsync
            => _onPlayerDeadAsyncSubject;

        /// <summary>
        /// 用於玩家死亡通知的資料流
        /// </summary>
        private readonly AsyncSubject<Unit> _onPlayerDeadAsyncSubject
            = new AsyncSubject<Unit>();

        /// <summary>
        /// 血量
        /// </summary>
        [SerializeField] private int _health = 10;

        /// <summary>
        /// 造成傷害
        /// </summary>
        public void ApplyDamage(int damageValue)
        {
            _health = Math.Max(0, _health - damageValue);

            if (_health == 0)
            {
                _onPlayerDeadAsyncSubject.OnNext(Unit.Default);

                // 推送OnCompleted
                _onPlayerDeadAsyncSubject.OnCompleted();
            }
        }
    }
}