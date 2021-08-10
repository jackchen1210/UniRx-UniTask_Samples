using System;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Samples.Section1.Async
{
    /// <summary>
    ///從指定的 URI 下載紋理並將其設置為 RawImage
    /// </summary>
    public class DownloadTextureUniTask : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private string uri;

        private void Start()
        {
            // 獲取與此 GameObject 關聯的 CancellationToken
            var token = this.GetCancellationTokenOnDestroy();

            // 執行紋理設置
            SetupTextureAsync(token).Forget();
        }

        private async UniTaskVoid SetupTextureAsync(CancellationToken token)
        {
            try
            {

                // 我想使用 UniRx 的 Retry，所以從 UniTask 轉換為 Observable
                var observable = Observable
                    .Defer(() =>
                    {
                        // UniTask -> IObservable
                        return GetTextureAsync(uri, token)
                            .ToObservable();
                    })
                    .Retry(3);

                // Observable 也可以使用 await
                var texture = await observable;

                _rawImage.texture = texture;
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                Debug.LogError(e);
            }
        }


        /// <summary>
        /// 使用async/await代替協程的結果是UniTask <Texture>
        /// </summary>
        private async UniTask<Texture> GetTextureAsync(
            string uri,
            CancellationToken token)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                await uwr.SendWebRequest().WithCancellation(token);
                if (uwr.result == UnityWebRequest.Result.ConnectionError
                    || uwr.result == UnityWebRequest.Result.DataProcessingError
                    || uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    // 失敗時發出異常
                    throw new Exception(uwr.error);
                }

                return ((DownloadHandlerTexture) uwr.downloadHandler).texture;
            }
        }
    }
}