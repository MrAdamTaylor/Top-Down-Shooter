using EnterpriceLogic.Constants;
using Infrastructure;
using Infrastructure.ServiceLocator;
using Infrastructure.StateMachine.States;
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
            _isActive = true;
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

        private bool UpCooldown()
        {
            return _unworkingCooldown <= 0f;
        }

        private void CoolDown()
        {
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
            GameObject bootstraper = GameObject.Find(ConstantsSceneObjects.GAME_BOOTSTRAPER);
            
            _gameTimeStoper.ResumeTime();
            _sceneLoader.LoadWithFinish(Constants.MAIN_MENU_SCENE, bootstraper);
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
