using UnityEngine;

public class GameBootstraper : MonoBehaviour, ICoroutineRunner
{
    public LoadingCurtain Curtain;
    private Game _game;
    
    public void StartGame()
    {
        _game = new Game(this,Curtain);
        _game.StateMachine.Enter<BootstrapState>();
        
        DontDestroyOnLoad(this);
    }
}