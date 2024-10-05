public class Game
{
    public GameStateMachine StateMachine;
    
    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, LevelConfigs levelConfigs)
    {
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container,levelConfigs);
    }
}