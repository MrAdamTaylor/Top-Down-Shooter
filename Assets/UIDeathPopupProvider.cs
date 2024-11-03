using System;
using Infrastructure.StateMachine.States;
using Logic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using UnityEngine.SceneManagement;

public class UIDeathPopupProvider : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    private GameSystem _gameSystem;
    public void Start()
    {
        _restartButton.onClick.AddListener(delegate { ExampleOpenRewardAd(1); });
    }

    public void Construct(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;
        SubscribeRestart();
        SubscribeMainMenu();
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(_gameSystem.ReloadGame);
        _mainMenuButton.onClick.RemoveListener(_gameSystem.MainMenu);
    }

    private void SubscribeRestart()
    {
        _restartButton.onClick.AddListener(_gameSystem.ReloadGame);
    }

    private void SubscribeMainMenu()
    {
        _mainMenuButton.onClick.AddListener(_gameSystem.MainMenu);
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

    }

    // Метод для вызова видео рекламы
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }
    public void LoadSceneAgain()
    {
        //Вот это убрать, и прописать потом закрытие окна и добавление хп
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
    
    

