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
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // Переключаем активность панели
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
            //NOTE - Destroy FadeObject and GameObject
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

        
        // Подписываемся на событие открытия рекламы в OnEnable
        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Rewarded;
        }

        // Отписываемся от события открытия рекламы в OnDisable
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
        }

        // Подписанный метод получения награды
        void Rewarded(int id)
        {
            if (id == 1)
                LoadSceneAgain();
            Debug.Log($"<color=green>YGYGYG Reload Game</color>");

        }

        // Метод для вызова видео рекламы
        public void ExampleOpenRewardAd(int id)
        {
            // Вызываем метод открытия видео рекламы
            YandexGame.RewVideoShow(id);
        }
    }
}
