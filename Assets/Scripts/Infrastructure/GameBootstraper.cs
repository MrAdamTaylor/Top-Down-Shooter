using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private LevelConfigs _levelConfigs;
    [SerializeField] private LoadingCurtain _curtain;
    private Game _game;

    public void Awake()
    {
        var canvas = _curtain.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void StartGame()
    {
        _game = new Game(_curtain, _levelConfigs);
        _game.StateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}

