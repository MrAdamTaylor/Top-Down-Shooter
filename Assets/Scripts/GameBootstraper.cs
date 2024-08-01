using System.Collections;
using UnityEngine;

public class GameBootstraper : MonoBehaviour, ICoroutineRunner
{
    public LoadingCurtain Curtain;
    private Game _game;
    
    public void StartGame()
    {
        Debug.Log("Метод старт запустился");
        _game = new Game(this,Curtain);
        _game.StateMachine.Enter<BootstrapState>();
    }
}

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator coroutine);
}

public class Game
{
    public GameStateMachine StateMachine;
    
    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
    {
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
    }
}

public class SceneLoader
{
    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
        Debug.Log("Класс для загрузки сцен инициализирован!");
    }
}