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
        public GameObject panelSound;
        public GameObject panelPause;
        [SerializeField] private GameObject CursorScript;

        private bool _isActive; // Указывает, активно ли меню паузы
        private float _currentTimeScale; // Сохраняет исходную скорость времени
        private bool _isBlocking; // Блокирует меню, если активирован "BlockAll"

        private GameTimeStoper _gameTimeStoper;
        private ISceneLoader _sceneLoader;

        public void Construct(ISceneLoader sceneLoader, GameTimeStoper gameTimeStoper)
        {
            _sceneLoader = sceneLoader;
            _gameTimeStoper = gameTimeStoper;
        }

        private void Awake()
        {
            _isActive = false;
            _currentTimeScale = Time.timeScale;
            ServiceLocator.Instance.BindData(typeof(UIPauseManager), this);
        }

        private void Update()
        {
            // Проверяем нажатие Tab для переключения меню паузы
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TogglePauseMenu();
            }
        }

        public void BlockAll()
        {
            _isBlocking = true;

            if (panelPause.activeSelf)
                panelPause.SetActive(false);

            if (panelSound.activeSelf)
                panelSound.SetActive(false);
        }

        public void TogglePauseMenu()
        {
            if (_isBlocking)
                return;

            _isActive = !_isActive;

            if (_isActive)
            {
                // Включаем паузу
                Time.timeScale = 0f;
                panelPause.SetActive(true);
                Cursor.visible = true;
                CursorScript.SetActive(false);
            }
            else
            {
                // Выключаем паузу
                ResumeGame();
            }
        }

        public void ResumeGame()
        {
            _isActive = false;
            Time.timeScale = _currentTimeScale;
            panelPause.SetActive(false);
            Cursor.visible = false;
            CursorScript.SetActive(true);
        }

        public void OpenPanelSound()
        {
            if (_isBlocking)
                return;

            panelSound.SetActive(true);
        }

        public void ClosePanelSound()
        {
            ResumeGame();
            panelSound.SetActive(false);
        }

        public void LoadSceneMenu()
        {
            GameBootstraper gameBootstraper = (GameBootstraper)ServiceLocator.Instance.GetData(typeof(GameBootstraper));

            _gameTimeStoper.ResumeTime();
            _sceneLoader.LoadWithFinish(Constants.MAIN_MENU_SCENE, gameBootstraper.gameObject);
            YandexGame.FullscreenShow();
        }

        public void ClosePanelPause()
        {
            ResumeGame();
        }

        public void UnblockAll()
        {
            _isBlocking = false;
        }
    }
}
