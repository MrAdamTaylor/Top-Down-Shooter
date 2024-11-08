using System;
using Configs;
using Infrastructure.Services;
using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game : IDisposable
    {
        public GameStateMachine StateMachine;
    
        public Game(ISceneLoader sceneLoader, LoadingCurtain curtain, LevelConfigs levelConfigs)
        {
            DispoceList.Instance.Add(this);
            StateMachine = new GameStateMachine(sceneLoader, curtain, AllServices.Container,levelConfigs);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}