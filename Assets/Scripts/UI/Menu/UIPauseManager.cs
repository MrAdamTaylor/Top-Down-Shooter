using EnterpriceLogic.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace UI.Menu
{
    public class UIPauseManager : MonoBehaviour
    {
        public GameObject panelSound;
        public GameObject panelPause;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                panelPause.SetActive(!panelPause.activeSelf);
            }
        }

        public void OpenPanelSound()
        {
            panelSound.SetActive(true);
        }

        public void ClosePanelSound()
        {
            panelSound.SetActive(false);
        }
        public void LoadSceneMenu()
        {
            GameObject bootstraper = GameObject.Find(ConstantsSceneObjects.GAME_BOOTSTRAPER);
            GameObject loadCurtain = GameObject.Find(ConstantsSceneObjects.GAME_LOAD_CURTAIN);
            Destroy(bootstraper);
            Destroy(loadCurtain);
            SceneManager.LoadScene(0);
        }

        public void LoadSceneAgain()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        public void ClosePanelPause()
        {
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
            Debug.Log($"<color=green>YGYGYG Reload Game</color>");

        }

        public void ExampleOpenRewardAd(int id)
        {
            YandexGame.RewVideoShow(id);
        }
    }
}
