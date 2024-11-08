using Configs;
using Infrastructure.Services;
using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
    
        public Game(ISceneLoader sceneLoader, LoadingCurtain curtain, LevelConfigs levelConfigs)
        {
            StateMachine = new GameStateMachine(sceneLoader, curtain, AllServices.Container,levelConfigs);
        }
    }
}