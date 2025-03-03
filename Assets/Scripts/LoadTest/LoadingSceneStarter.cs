using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadTest
{
    public class LoadingSceneStarter : MonoBehaviour
    {
        [SerializeField]private ScreenLoader _sceneLoaderScreen;
        readonly CancellationTokenSource cts = new CancellationTokenSource();

        public void Start()
        {
            _ = StartAsync(cts.Token);
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            await Resources.UnloadUnusedAssets();
            Debug.Log($"<color=yellow> Loading Scene Async </color>");
            var op = SceneManager.LoadSceneAsync("Scripts/LoadTest/NextScene");
            await op.ToUniTask(Progress.Create<float>(x => _sceneLoaderScreen.SetProgress("Loading...", x)));
        }
    }
}