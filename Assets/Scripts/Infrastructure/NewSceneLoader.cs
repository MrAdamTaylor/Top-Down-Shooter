using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class NewSceneLoader : MonoBehaviour, ISceneLoader
    {
        private string _nextScene;
        private AsyncOperation _asyncOperation;
        private bool _isLoaded;
        private bool _isIntemediateScene;
        private Action _action;
        private Coroutine _routine;
        private bool _isFinalSceneLoaded;

        public void Construct(string sceneName)
        {
            _nextScene = sceneName;
        }

        public void Load(string gameScene)
        {
            throw new NotImplementedException();
        }

        private void Start()
        {
            /*if (SceneManager.GetActiveScene().name == "Intermediate_Scene")
                StartCoroutine(nameof(LoadPressKey), PlayerPrefs.GetString("current_scene"));*/
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (_isIntemediateScene)
            {
                if (SceneManager.GetActiveScene().name == "Intermediate_Scene")
                {
                    _isIntemediateScene = false;
                    //_action.Invoke();
                    _routine = StartCoroutine(nameof(LoadPressKey), PlayerPrefs.GetString("current_scene"));
                }
            }

            /*if (_isIntemediateScene)
            {
                _isIntemediateScene = false;
                _routine = StartCoroutine(nameof(LoadPressKey), PlayerPrefs.GetString("current_scene"));
            }*/
            //if (SceneManager.GetActiveScene().name == "Intermediate_Scene")
                
            
            if (_isLoaded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopCoroutine(_routine);
                    _asyncOperation.allowSceneActivation = true;
                    _isLoaded = false;
                    _isFinalSceneLoaded = true;
                }
            }

            if (_isFinalSceneLoaded)
            {
                if (SceneManager.GetActiveScene().name == _nextScene)
                {
                    _action.Invoke();
                    _isFinalSceneLoaded = false;
                }
            } 
        }
        
        

        public void Load(string gameScene, Action onLoaded = null)
        {
            LoadIntermediateScene(gameScene, onLoaded);
        }

        private void LoadIntermediateScene(string nextScene, Action action)
        {
            _nextScene = nextScene;
            _action = action;
            PlayerPrefs.SetString("current_scene", nextScene);
            SceneManager.LoadScene("Intermediate_Scene");
            _isIntemediateScene = true;
        }

        private IEnumerator LoadPressKey(string sceneName)
        {
            _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            _asyncOperation.allowSceneActivation = false;
            while (_asyncOperation.progress < 0.9f)
            {
                yield return true;
            }
            //_isIntemediateScene = false;
            _isLoaded = true;
            Debug.Log("<color=yellow>Load is Ended!</color>");
            //StopCoroutine(_routine);
        }
    }
}

