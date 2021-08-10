using UniRx;
using UnityEngine;

namespace Samples.Section2.Operators
{
    /// <summary>
    /// 過濾消息的範例
    /// </summary>
    public class OperatorTest : MonoBehaviour
    {
        void Start()
        {
            var subject = new Subject<int>();

            // 一般訂閱
            subject.Subscribe(x => Debug.Log("raw:" + x));

            // 訂閱不包括 0 或更少
            subject.Where(x => x > 0).Subscribe(x => Debug.Log("filter:" + x));

            // 推送數值
            subject.OnNext(1);
            subject.OnNext(-1);
            subject.OnNext(3);
            subject.OnNext(0);

            // 結束
            subject.OnCompleted();
        }
    }
}