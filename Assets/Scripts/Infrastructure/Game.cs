public class Game
{
    public GameStateMachine StateMachine;
    
    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, GameParams gameParams)
    {
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container,gameParams);
    }
}