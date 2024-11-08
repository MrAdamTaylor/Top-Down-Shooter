using System;
using System.Collections;
using EnterpriceLogic.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class AsyncSceneLoader : MonoBehaviour, ISceneLoader
    {
        private string _nextScene;
        private Coroutine _routine;
        private AsyncOperation _asyncOperation;
        private bool _isLoaded;
        private Action _action;
        private bool _isFinal;
        private bool _isConfigured;
        private bool _canLoad;

        public void Construct(string nextScene)
        {
            _nextScene = nextScene;
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("<color=green>Move</color>");*/
                if (_isLoaded && !_isFinal)
                {
                    _asyncOperation.allowSceneActivation = true;
                    _action?.Invoke();
                    _isFinal = true;
                }
            //}
        }

        public void Load(string gameScene)
        {
            if (gameScene == Constants.INTERMEDIATE_SCENE)
            {
                SceneManager.LoadScene("Intermediate_Scene");
            }
        }

        public void Load(string gameScene, Action onLoaded)
        {
            _action = onLoaded;
            if (gameScene == _nextScene)
            {
                _routine = StartCoroutine(nameof(LoadPressKey));
            }
        }
        
        private IEnumerator LoadPressKey()
        {
            _asyncOperation = SceneManager.LoadSceneAsync(_nextScene);
            _asyncOperation.allowSceneActivation = false;
            while (_asyncOperation.progress < 0.9f)
            {
                yield return true;
            }
            _isLoaded = true;
        }
        
    }
}