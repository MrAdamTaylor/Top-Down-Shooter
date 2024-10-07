using System;
using UnityEngine;

public class GameBootstraper : MonoBehaviour, ICoroutineRunner
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
        _game = new Game(this,_curtain, _levelConfigs);
        _game.StateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}

