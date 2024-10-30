using System;
using Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

public class UIDeathPopupProvider : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    private GameSystem _gameSystem;

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
    
    
}
