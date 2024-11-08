using EnterpriceLogic.Constants;
using Infrastructure;
using Infrastructure.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace UI.Menu
{
    public class UIPauseManager : MonoBehaviour
    {
        public GameObject panelSound;
        public GameObject panelPause;

        private bool _isActive;
        private float _currentTimeScale;
        private Player.Player _player;

        public void Construct()
        {
            _player = (Player.Player)ServiceLocator.Instance.GetData(typeof(Player.Player));
        }

        private void Awake()
        {
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
                /*if(panelPause.activeSelf)
                    _player.Blocked();
                else
                    _player.UnBlocked();*/
                panelPause.SetActive(!panelPause.activeSelf);
            }
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
            //LoadingCurtain loadCurtain = GameObject.FindObjectOfType<LoadingCurtain>();
            //Debug.Log(loadCurtain +"<color=cyan> Is finded </color>");
            //GameObject loadCurtain = GameObject.Find(ConstantsSceneObjects.GAME_LOAD_CURTAIN);
            //Destroy(bootstraper);
            //Destroy(loadCurtain);
            SceneManager.LoadScene(0);
        }

        public void LoadSceneAgain()
        {
            /*Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);*/
        }

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
            if (id == 1)
                LoadSceneAgain();
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
