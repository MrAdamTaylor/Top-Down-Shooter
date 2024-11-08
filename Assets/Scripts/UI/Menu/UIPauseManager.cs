using EnterpriceLogic.Constants;
using Infrastructure;
using Infrastructure.ServiceLocator;
using Infrastructure.StateMachine.States;
using Logic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace UI.Menu
{
    public class UIPauseManager : MonoBehaviour
    {
        public const float TIME_BEFORE_WORKING = 5f;
        
        public GameObject panelSound;
        public GameObject panelPause;

        private bool _isActive;
        private float _currentTimeScale;
        private bool _coolDownIsEnd;

        private GameTimeStoper _gameTimeStoper;

        private ISceneLoader _sceneLoader;
        private float _unworkingCooldown;

        public void Construct(ISceneLoader sceneLoader, GameTimeStoper gameTimeStoper)
        {
            _sceneLoader = sceneLoader;
            _gameTimeStoper = gameTimeStoper;
        }

        private void Awake()
        {
            _unworkingCooldown = TIME_BEFORE_WORKING;
            _isActive = false;
            _currentTimeScale = Time.timeScale;
            ServiceLocator.Instance.BindData(typeof(UIPauseManager), this);
        }
        
        private bool _isBlocking;

        public void BlockAll()
        {
            _isBlocking = true;
			
			if(panelPause.activeSelf)
                panelPause.SetActive(false);
            
            if(panelSound.activeSelf)
                panelSound.SetActive(false);
        }

        void Update()
        {
            UpdateCooldown();
            if (_unworkingCooldown <= 0f)
            {
                _coolDownIsEnd = true;
                _isActive = true;
            }

            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(_isBlocking)
                    return;
                
                if (_isActive)
                    Time.timeScale = 0;
                else
                    Time.timeScale = _currentTimeScale;
                _isActive = !_isActive;
                panelPause.SetActive(!panelPause.activeSelf);
            }
        }

        private void UpdateCooldown()
        {
            if (_coolDownIsEnd)
                return;
            _unworkingCooldown -= Time.deltaTime;
        }

        public void OpenPanelSound()
        {   
            if(_isBlocking)
                return;
            
            panelSound.SetActive(true);
        }

        public void ClosePanelSound()
        {
            _isActive = true;
            Time.timeScale = _currentTimeScale;
            panelSound.SetActive(false);
        }
        public void LoadSceneMenu()
        {
            GameBootstraper gameBootstraper = (GameBootstraper)ServiceLocator.Instance.GetData(typeof(GameBootstraper));
            
            _gameTimeStoper.ResumeTime();
            _sceneLoader.LoadWithFinish(Constants.MAIN_MENU_SCENE, gameBootstraper.gameObject);
        }

        /*public void LoadSceneAgain()
        {
            /*Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);#1#
        }*/

        public void ClosePanelPause()
        {
            _isActive = true;
            Time.timeScale = _currentTimeScale;
            panelPause.SetActive(!panelPause.activeSelf);
        }

        
        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Rewarded;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
        }

        void Rewarded(int id)
        {
            //if (id == 1)
                //LoadSceneAgain();
            //Debug.Log($"<color=green>YGYGYG Reload Game</color>");

        }

        /*public void ExampleOpenRewardAd(int id)
        {
            YandexGame.RewVideoShow(id);
        }*/

        public void UnblockAll()
        {
            _isBlocking = false;
        }
    }
}
