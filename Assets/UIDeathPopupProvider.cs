using Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

public class UIDeathPopupProvider : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    private GameSystem _gameSystem;

    private void Construct(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;
    }

    public void SubscribeRestart()
    {
        
    }

    public void SubscribeMainMenu()
    {
        
    }
}
