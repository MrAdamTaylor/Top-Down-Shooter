using UnityEngine;

public class GameBootstraper : MonoBehaviour, ICoroutineRunner
{
    public LoadingCurtain Curtain;
    private Game _game;
    [SerializeField] private GameParams _gameParams;
    
    public void StartGame()
    {
        _game = new Game(this,Curtain, _gameParams);
        _game.StateMachine.Enter<BootstrapState>();
        
        DontDestroyOnLoad(this);
    }
}

[System.Serializable]
public struct GameParams
{
    public Level ELevel;
    public PlayerType EPlayer;
}

public enum Level
{
    Level1,
    Level2,
    Level3
}

public enum PlayerType
{
    Player1,
    Player2
}