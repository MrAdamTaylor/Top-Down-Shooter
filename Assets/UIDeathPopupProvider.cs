using System;
using Infrastructure.StateMachine.States;
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

    // ������������� �� ������� �������� ������� � OnEnable
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // ������������ �� ������� �������� ������� � OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // ����������� ����� ��������� �������
    void Rewarded(int id)
    {
        if (id == 1)
            LoadSceneAgain();

    }

    // ����� ��� ������ ����� �������
    public void ExampleOpenRewardAd(int id)
    {
        // �������� ����� �������� ����� �������
        YandexGame.RewVideoShow(id);
    }
    public void LoadSceneAgain()
    {
        //��� ��� ������, � ��������� ����� �������� ���� � ���������� ��
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
    
    

