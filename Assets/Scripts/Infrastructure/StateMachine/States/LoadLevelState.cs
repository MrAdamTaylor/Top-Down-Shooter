using System;
using Configs;
using Infrastructure;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.Interfaces;
using Infrastructure.StateMachine.States;
using UnityEngine;

public class LoadLevelState : IState, IDisposable
{
    private GameStateMachine _stateMachine;
    private ISceneLoader _sceneLoader;
    private LevelConfigs _levelConfigs;
    
    public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, LevelConfigs levelConfigs)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _levelConfigs = levelConfigs;
    }

    public void Exit()
    {
    }

    public void Enter()
    {
        Debug.Log("<color=red>State after asyncLoad</color>");
        _stateMachine.Enter<GameLoopState>();
    }

    public void Dispose()
    {
        
    }
}