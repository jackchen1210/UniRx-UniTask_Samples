using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section1
{
    public class DownloadTexture : MonoBehaviour
    {
        /// <summary>
        /// uGUI的RawImage
        /// </summary>
        [SerializeField] private RawImage _rawImage;
        /// <summary>
        /// 要顯示的圖像路徑
        /// </summary>
        [SerializeField] private string uri;

        private void Start()
        {

            // 獲得紋理
            // 但是如果發生異常，最多嘗試 3 次。
            GetTextureAsync(uri)
                .OnErrorRetry(
                    onError: (Exception _) => { },
                    retryCount: 3
                ).Subscribe(
                    result => { _rawImage.texture = result; },
                    error => { Debug.LogError(error); }
                ).AddTo(this);
        }

        /// <summary>
        /// 調用協程並將結果作為 Observable 返回
        /// </summary>
        private IObservable<Texture> GetTextureAsync(string uri)
        {
            return Observable
                .FromCoroutine<Texture>(observer =>
                {
                    return GetTextureCoroutine(observer, uri);
                });
        }

        /// <summary>
        /// 使用協程下載紋理
        /// </summary>
        private IEnumerator GetTextureCoroutine(IObserver<Texture> observer, string uri)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                yield return uwr.SendWebRequest();
                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    // 如果發生錯誤，則發出 OnError 消息
                    observer.OnError(new Exception(uwr.error));
                    yield break;
                }

                var result = ((DownloadHandlerTexture) uwr.downloadHandler).texture;
                // 成功時發出 OnNext / OnCompleted 消息
                observer.OnNext(result);
                observer.OnCompleted();
            }
        }
    }
}