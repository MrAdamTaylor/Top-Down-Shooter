using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        public void Construct(string sceneName)
        {
            throw new NotImplementedException();
        }

        public void Load(string gameScene)
        {
            throw new NotImplementedException();
        }

        public void Load(string gameScene, Action onLoaded = null)
        {
            _ = LoadScene(gameScene, onLoaded);
        }

        private async UniTaskVoid LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            await SceneManager.LoadSceneAsync(nextScene);

            onLoaded?.Invoke();
        }

        
    }
}