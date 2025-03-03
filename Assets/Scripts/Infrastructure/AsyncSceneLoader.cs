using System;
using System.Collections;
using EnterpriceLogic.Constants;
using Infrastructure.BootstrapLogic;
using Infrastructure.ServiceLocator;
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

        private static AsyncSceneLoader _instance;

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
                if (_isLoaded && !_isFinal)
                {
                    _asyncOperation.allowSceneActivation = true;
                    _action?.Invoke();
                    _isFinal = true;
                }
        }

        public void Load(string gameScene)
        {
            if (gameScene == Constants.INTERMEDIATE_SCENE)
            {
                SceneManager.LoadScene(Constants.INTERMEDIATE_SCENE, LoadSceneMode.Single);
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

        public void LoadWithFinish(string mainMenuScene, GameObject bootstraper)
        {
            SceneManager.LoadScene(Constants.INTERMEDIATE_SCENE, LoadSceneMode.Single);
            DispoceList.Instance.RemoveAll();
            ServiceLocator.ServiceLocator.Instance.CleanAllData();
            Destroy(bootstraper);
            SceneManager.LoadScene(Constants.MAIN_MENU_SCENE, LoadSceneMode.Single);
            Destroy(gameObject);
        }

        private IEnumerator LoadPressKey()
        {
            _asyncOperation = SceneManager.LoadSceneAsync(_nextScene, LoadSceneMode.Single);
            _asyncOperation.allowSceneActivation = false;
            while (_asyncOperation.progress < 0.9f)
            {
                yield return true;
            }
            _isLoaded = true;
        }
        
    }
}