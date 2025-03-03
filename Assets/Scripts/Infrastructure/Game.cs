using System;
using Configs;
using Infrastructure.DIConteiner;
using Infrastructure.ServiceLocator;
using Infrastructure.Services;
using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game : IDisposable
    {
        public GameStateMachine StateMachine;
    
        public Game(ISceneLoader sceneLoader, LevelConfigs levelConfigs, GameInjector gameInjector)
        {
            DispoceList.Instance.Add(this);
            StateMachine = new GameStateMachine(sceneLoader,  AllServices.Container,levelConfigs, gameInjector);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}