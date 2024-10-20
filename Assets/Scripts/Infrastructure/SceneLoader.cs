using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void Load(string name, Action onLoaded = null)
    {
        LoadScene(name, onLoaded);
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